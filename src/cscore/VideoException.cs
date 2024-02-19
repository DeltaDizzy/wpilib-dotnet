using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CsCore;

public class VideoException(string msg) : Exception(msg)
{
    public override string ToString()
    {
        return $"VideoException [{base.ToString()}]";
    }

    [DoesNotReturn]
    public static void ThrowException(StatusValue status)
    {
        throw new VideoException(status.ToString());
    }
}
