﻿// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

using NetworkTables.Handles;
using NetworkTables.Natives;

namespace NetworkTables;

/** NetworkTables BooleanArray implementation. */
internal sealed class BooleanArrayEntryImpl<T> : EntryBase<T>, IBooleanArrayEntry where T : struct, INtEntryHandle
{
    /**
     * Constructor.
     *
     * @param topic Topic
     * @param handle Native handle
     * @param defaultValue Default value for Get()
     */
    internal BooleanArrayEntryImpl(BooleanArrayTopic topic, T handle, bool[] defaultValue) : base(handle)
    {
        Topic = topic;
        m_defaultValue = defaultValue;
    }

    public override BooleanArrayTopic Topic { get; }


    public bool[] Get()
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (value.IsBooleanArray)
        {
            return value.GetBooleanArray();
        }
        return m_defaultValue;
    }


    public bool[] Get(bool[] defaultValue)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        if (value.IsBooleanArray)
        {
            return value.GetBooleanArray();
        }
        return defaultValue;
    }


    public TimestampedBooleanArray GetAtomic()
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        bool[] baseValue = value.IsBooleanArray ? value.GetBooleanArray() : m_defaultValue;
        return new TimestampedBooleanArray(value.Time, value.ServerTime, baseValue);
    }


    public TimestampedBooleanArray GetAtomic(bool[] defaultValue)
    {
        NetworkTableValue value = NtCore.GetEntryValue(Handle);
        bool[] baseValue = value.IsBooleanArray ? value.GetBooleanArray() : defaultValue;
        return new TimestampedBooleanArray(value.Time, value.ServerTime, baseValue);
    }


    public TimestampedBooleanArray[] ReadQueue()
    {
        NetworkTableValue[] values = NtCore.ReadQueueValue(Handle);
        TimestampedBooleanArray[] timestamped = new TimestampedBooleanArray[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            timestamped[i] = new TimestampedBooleanArray(values[i].Time, values[i].ServerTime, values[i].GetBooleanArray());
        }
        return timestamped;
    }


    public bool[][] ReadQueueValues()
    {
        NetworkTableValue[] values = NtCore.ReadQueueValue(Handle);
        bool[][] timestamped = new bool[values.Length][];
        for (int i = 0; i < values.Length; i++)
        {
            timestamped[i] = values[i].GetBooleanArray();
        }
        return timestamped;
    }

    public void Set(bool[] value, long time)
    {
        NetworkTableValue ntValue = NetworkTableValue.MakeBooleanArray(value, time);
        NtCore.SetEntryValue(Handle, ntValue);
    }

    public void SetDefault(bool[] value)
    {
        NetworkTableValue ntValue = NetworkTableValue.MakeBooleanArray(value);
        NtCore.SetDefaultEntryValue(Handle, ntValue);
    }

    public void Unpublish()
    {
        NtCore.Unpublish(Handle);
    }

    private readonly bool[] m_defaultValue;
}
