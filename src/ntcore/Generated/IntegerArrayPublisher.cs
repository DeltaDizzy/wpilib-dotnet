// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using System;

namespace NetworkTables;

/// <summary>
/// NetworkTables IntegerArray publisher.
/// </summary>
public interface IIntegerArrayPublisher : IPublisher
{
    /// <summary>
    /// Gets the corresponding topic.
    /// </summary>
    new IntegerArrayTopic Topic { get; }

    /// <summary>
    /// Publish a new value using the current NT time.
    /// </summary>
    /// <param name="value">value to publish</param>
    void Set(ReadOnlySpan<long> value);

    /// <summary>
    /// Publish a new value.
    /// </summary>
    /// <param name="value">value to publish</param>
    /// <param name="time">timestamp; 0 indicates current NT time should be used</param>
    void Set(ReadOnlySpan<long> value, long time);

    /// <summary>
    /// Publish a default value. On reconnect, a default value will never be used
    /// in prference to a published value
    /// </summary>
    /// <param name="value">value</param>
    void SetDefault(ReadOnlySpan<long> value);
}
