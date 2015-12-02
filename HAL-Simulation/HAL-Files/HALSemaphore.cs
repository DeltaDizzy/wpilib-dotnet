﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using HAL;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591

namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALSemaphore
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALSemaphore.InitializeMutexNormal = initializeMutexNormal;
            global::HAL.HALSemaphore.DeleteMutex = deleteMutex;
            global::HAL.HALSemaphore.TakeMutex = takeMutex;
            global::HAL.HALSemaphore.TryTakeMutex = tryTakeMutex;
            global::HAL.HALSemaphore.GiveMutex = giveMutex;
            global::HAL.HALSemaphore.InitializeMultiWait = initializeMultiWait;
            global::HAL.HALSemaphore.DeleteMultiWait = deleteMultiWait;
            global::HAL.HALSemaphore.TakeMultiWait = takeMultiWait;
            global::HAL.HALSemaphore.GiveMultiWait = giveMultiWait;
        }


        [CalledSimFunction]
        public static IntPtr initializeMutexRecursive()
        {
            MUTEX_ID p = new MUTEX_ID { lockObject = new object() };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static IntPtr initializeMutexNormal()
        {
            MUTEX_ID p = new MUTEX_ID { lockObject = new object() };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void deleteMutex(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void takeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            Monitor.Enter(temp.lockObject);
        }

        [CalledSimFunction]
        public static bool tryTakeMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);
            return retVal;
        }

        [CalledSimFunction]
        public static void giveMutex(IntPtr sem)
        {
            var temp = (MUTEX_ID)Marshal.PtrToStructure(sem, typeof(MUTEX_ID));
            Monitor.Exit(temp.lockObject);
        }

        [CalledSimFunction]
        public static IntPtr initializeSemaphore(uint initial_value)
        {
            var p = new MULTIWAIT_ID {lockObject = new object()};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }


        [CalledSimFunction]
        public static void deleteSemaphore(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        [CalledSimFunction]
        public static sbyte takeSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            Monitor.Enter(temp.lockObject);//temp.semaphore.WaitOne();
            return 0;
        }

        [CalledSimFunction]
        public static sbyte tryTakeSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            bool retVal = Monitor.TryEnter(temp.lockObject);

            return retVal ? (sbyte)0 : (sbyte)1;
        }

        [CalledSimFunction]
        public static sbyte giveSemaphore(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            if (Monitor.IsEntered(temp.lockObject))
                Monitor.Exit(temp.lockObject);
            return 0;
        }

        [CalledSimFunction]
        public static IntPtr initializeMultiWait()
        {
            MULTIWAIT_ID p = new MULTIWAIT_ID { lockObject = new object() };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(p));
            Marshal.StructureToPtr(p, ptr, true);

            return ptr;
        }

        [CalledSimFunction]
        public static void deleteMultiWait(IntPtr sem)
        {
            Marshal.FreeHGlobal(sem);
            sem = IntPtr.Zero;
        }

        [CalledSimFunction]
        public static void takeMultiWait(IntPtr sem, IntPtr m)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));

            lock (temp.lockObject)
            {
                try
                {
                    Monitor.Wait(temp.lockObject);
                }
                catch (ThreadInterruptedException)
                {
                }
            }
        }

        [CalledSimFunction]
        public static void giveMultiWait(IntPtr sem)
        {
            var temp = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            lock (temp.lockObject)
            {
                try
                {
                    Monitor.PulseAll(temp.lockObject);
                }
                catch (ThreadInterruptedException)
                {

                }
            }
        }
    }
}
