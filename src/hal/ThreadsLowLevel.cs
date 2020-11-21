﻿
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class ThreadsLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static ThreadsLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int GetCurrentThreadPriority(int* isRealTime)
        {
            return lowLevel.HAL_GetCurrentThreadPriority(isRealTime);
        }

        public static int GetThreadPriority(void* handle, int* isRealTime)
        {
            return lowLevel.HAL_GetThreadPriority(handle, isRealTime);
        }

        public static int SetCurrentThreadPriority(int realTime, int priority)
        {
            return lowLevel.HAL_SetCurrentThreadPriority(realTime, priority);
        }

        public static int SetThreadPriority(void* handle, int realTime, int priority)
        {
            return lowLevel.HAL_SetThreadPriority(handle, realTime, priority);
        }

    }
}
