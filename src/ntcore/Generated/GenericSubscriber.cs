﻿// Copyright (c) FIRST and other WPILib contributors.
// Open Source Software; you can modify and/or share it under the terms of
// the WPILib BSD license file in the root directory of this project.

// THIS FILE WAS AUTO-GENERATED BY ./ntcore/generate_topics.py. DO NOT MODIFY

namespace NetworkTables;

/** NetworkTables generic subscriber. */
public partial interface IGenericSubscriber
{

    /**
     * Gets the entry's value as a bool. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    bool GetBoolean(bool defaultValue);

    /**
     * Gets the entry's value as a long. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    long GetInteger(long defaultValue);

    /**
     * Gets the entry's value as a float. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    float GetFloat(float defaultValue);

    /**
     * Gets the entry's value as a double. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    double GetDouble(double defaultValue);

    /**
     * Gets the entry's value as a string. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    string GetString(string defaultValue);

    /**
     * Gets the entry's value as a byte[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    byte[] GetRaw(byte[] defaultValue);

    /**
     * Gets the entry's value as a bool[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    bool[] GetBooleanArray(bool[] defaultValue);

    /**
     * Gets the entry's value as a long[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    long[] GetIntegerArray(long[] defaultValue);

    /**
     * Gets the entry's value as a float[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    float[] GetFloatArray(float[] defaultValue);

    /**
     * Gets the entry's value as a double[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    double[] GetDoubleArray(double[] defaultValue);

    /**
     * Gets the entry's value as a string[]. If the entry does not exist or is of different type, it
     * will return the default value.
     *
     * @param defaultValue the value to be returned if no value is found
     * @return the entry's value or the given default value
     */
    string[] GetStringArray(string[] defaultValue);

}
