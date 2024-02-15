using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Monologue.SourceGenerator;

internal record ClassData
{
    public readonly EquatableArray<string> LoggedItems;
    public readonly string Name;
    public readonly string ClassDeclaration;
    public readonly string? Namespace;

    public ClassData(ImmutableArray<string> loggedItems, string name, string classDeclaration, string? ns)
    {
        LoggedItems = new (loggedItems);
        Name = name;
        ClassDeclaration = classDeclaration;
        Namespace = ns;
    }
}

[Generator]
public class LogGenerator : IIncrementalGenerator
{
    static ClassData? GetClassData(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax)
    {
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
        {
            return null;
        }

        var ns = classSymbol.ContainingNamespace?.ToDisplayString();
        StringBuilder typeBuilder = new StringBuilder();
        classSymbol.GetTypeDeclaration(typeBuilder);

        var classMembers = classSymbol.GetMembers();

        var loggableMembers = ImmutableArray.CreateBuilder<string>(classMembers.Length);

        foreach (var member in classMembers)
        {
            var attributes = member.GetAttributes();

            foreach (AttributeData attribute in attributes)
            {
                var attributeClass = attribute.AttributeClass;
                if (attributeClass is null)
                {
                    continue;
                }
                if (attributeClass.ToDisplayString() == "Monologue.LogAttribute")
                {
                    string getOperation;
                    string defaultPathName;
                    ITypeSymbol logType;
                    // This is ours
                    if (member is IFieldSymbol field)
                    {
                        getOperation = field.Name;
                        defaultPathName = field.Name;
                        logType = field.Type;
                    }
                    else if (member is IPropertySymbol property)
                    {
                        getOperation = property.Name;
                        defaultPathName = property.Name;
                        logType = property.Type;
                    }
                    else if (member is IMethodSymbol method)
                    {
                        if (method.ReturnsVoid)
                        {
                            throw new InvalidOperationException("Cannot have a void returning method");
                        }
                        if (!method.Parameters.IsEmpty)
                        {
                            throw new InvalidOperationException("Cannot take a parameter");
                        }

                        getOperation = $"{method.Name}()";
                        defaultPathName = method.Name;
                        logType = method.ReturnType;
                    }
                    else
                    {
                        throw new InvalidOperationException("Field is not loggable");
                    }

                    string fullOperation;

                    if (logType.AllInterfaces.Where(x => x.ToDisplayString() == "Monologue.ILogged").Any())
                    {
                        fullOperation = $"{getOperation}.UpdateMonologue($\"{{path}}/{defaultPathName}\", logger);";
                    }
                    else
                    {
                        // TODO the rest of the types
                        fullOperation = "";
                    }

                    loggableMembers.Add(fullOperation);


                    break;
                }
            }
        }

        return new ClassData(loggableMembers.ToImmutable(), classSymbol.Name, typeBuilder.ToString(), ns);
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "Monologue.GenerateLogAttribute",
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, _) => GetClassData(ctx.SemanticModel, ctx.TargetNode))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(attributedTypes,
            static (spc, source) => Execute(source, spc));
    }

    static void Execute(ClassData? classData, SourceProductionContext context)
    {
        if (classData is { } value)
        {
            StringBuilder builder = new StringBuilder();
            if (value.Namespace is not null)
            {
                builder.AppendLine($"namespace {value.Namespace};");
                builder.AppendLine();

                builder.Append(value.ClassDeclaration);
                builder.AppendLine(" : ILogged");
                builder.AppendLine("{");
                builder.AppendLine("    public void UpdateMonologue(string path, Monologue.Monologuer logger)");
                builder.AppendLine("    {");
                foreach (var call in value.LoggedItems)
                {
                    builder.AppendLine($"        {call}");
                }
                builder.AppendLine("    }");
                builder.AppendLine("}");
            }
            context.AddSource($"Monologue.{value.Name}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }

    private static object? GetDiagnosticIfInvalidClassForGeneration(TypeDeclarationSyntax syntax, ITypeSymbol symbol)
    {
        // Ensure class is partial
        if (!syntax.IsInPartialContext(out var nonPartialIdentifier))
        {
            return new object();
        }

        return null;
    }
}
