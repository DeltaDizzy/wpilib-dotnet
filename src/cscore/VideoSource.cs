using System;
using System.Collections.Generic;
using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class VideoSource : IDisposable, IEquatable<VideoSource?>
{
    public CsSource Handle { get; private set; }

    public bool IsValid => Handle.Handle != 0;

    protected VideoSource(CsSource handle)
    {
        Handle = handle;
    }

    public SourceKind Kind
    {
        get
        {
            var kind = CsNative.GetSourceKind(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return kind;
        }
    }

    public string Name
    {
        get
        {
            var name = CsNative.GetSourceName(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return name;
        }
    }

    public string Description
    {
        get
        {
            var description = CsNative.GetSourceDescription(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return description;
        }
    }

    public long LastFrameTime
    {
        get
        {
            var frameTime = CsNative.GetSourceLastFrameTime(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return (long)frameTime;
        }
    }

    public ConnectionStrategy ConnectionStrategy
    {
        set
        {
            CsNative.SetSourceConnectionStrategy(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public bool IsConnected
    {
        get
        {
            var isConnected = CsNative.IsSourceConnected(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return isConnected;
        }
    }

    public bool IsEnabled
    {
        get
        {
            var isEnabled = CsNative.IsSourceEnabled(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return isEnabled;
        }
    }

    public VideoMode VideoMode
    {
        get
        {
            CsNative.GetSourceVideoMode(Handle, out var mode, out var status);
            VideoException.ThrowIfFailed(status);
            return mode;
        }
        set
        {
            CsNative.SetSourceVideoMode(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public bool SetConfigJson(string config)
    {
        var ret = CsNative.SetSourceConfigJson(Handle, config, out var status);
        VideoException.ThrowIfFailed(status);
        return ret;
    }

    public string GetConfigJson()
    {
        var ret = CsNative.GetSourceConfigJson(Handle, out var status) ?? "";
        VideoException.ThrowIfFailed(status);
        return ret;
    }

    public void Dispose()
    {
        if (Handle.Handle != 0)
        {
            CsNative.ReleaseSource(Handle, out var status);
            VideoException.ThrowIfFailed(status);
        }
        Handle = default;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as VideoSource);
    }

    public bool Equals(VideoSource? other)
    {
        return other is not null &&
               Handle.Equals(other.Handle);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Handle);
    }

    public static bool operator ==(VideoSource? left, VideoSource? right)
    {
        return EqualityComparer<VideoSource>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoSource? left, VideoSource? right)
    {
        return !(left == right);
    }
}
