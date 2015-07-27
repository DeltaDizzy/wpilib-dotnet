﻿using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_RoboRIO
{
    internal class HALCAN
    {
        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_sendMessage")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_sendMessage(uint messageID, byte[] data,
            byte dataSize, int periodMs, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_receiveMessage")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref uint messageID,
            uint messageIDMask, byte[] data, ref byte dataSize, ref uint timeStamp, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_openStreamSession")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_openStreamSession(ref uint sessionHandle,
            uint messageID, uint messageIDMast, uint maxMessages, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_closeStreamSession")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_closeStreamSession(uint sessionHandle);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_readStreamSession")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_readStreamSession(uint sessionHandle,
            CANStreamMessage messages, uint messagesToRead, uint[] messagesRead, ref int status);

        [DllImport(HAL.LibhalathenaSharedSo,
            EntryPoint = "FRC_NetworkCommunication_CANSessionMux_getCANStatus")]
        internal static extern void FRC_NetworkCommunication_CANSessionMux_getCANStatus(ref float perfectButUtilization,
            ref uint busOffCount, ref uint txFullCount, ref uint recieveErrorCount, ref uint transmitErrorCount,
            ref int status);


    }
}
