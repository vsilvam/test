using System;
using System.Threading;

namespace App.Infrastructure.Runtime
{
    public class ISProcess
    {
        #region Delegates

        public delegate void DelegateAsyncMethod(object objState);

        #endregion

        #region ProcessPriority enum

        [Serializable]
        public enum ProcessPriority
        {
            // Summary:
            //     The System.Threading.Thread can be scheduled after threads with any other
            //     priority.
            Lowest = 0,
            //
            // Summary:
            //     The System.Threading.Thread can be scheduled after threads with Normal priority
            //     and before those with Lowest priority.
            BelowNormal = 1,
            //
            // Summary:
            //     The System.Threading.Thread can be scheduled after threads with AboveNormal
            //     priority and before those with BelowNormal priority. Threads have Normal
            //     priority by default.
            Normal = 2,
            //
            // Summary:
            //     The System.Threading.Thread can be scheduled after threads with Highest priority
            //     and before those with Normal priority.
            AboveNormal = 3,
            //
            // Summary:
            //     The System.Threading.Thread can be scheduled before threads with any other
            //     priority.
            Highest = 4,
        }

        #endregion

        public static bool StartAsyncProcess(DelegateAsyncMethod methodName, ProcessPriority threadPriority,
                                             object objState)
        {
            var objParameterizedThreadStart = new ParameterizedThreadStart(methodName);

            var objThread = new Thread(objParameterizedThreadStart, 0);
            objThread.Priority = (ThreadPriority) threadPriority;

            try
            {
                objThread.Start(objState);
                return true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                return false;
            }
        }

        public static void PauseCurrentProcess(int seconds)
        {
            Thread.Sleep(seconds*1000);
        }
    }
}