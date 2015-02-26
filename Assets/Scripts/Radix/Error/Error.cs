using Radix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radix.Utlities;
using System.Diagnostics;

namespace Radix.Error
{
    public class Error
    {
        private Error() { }

        public static void Create(string pMessage, EErrorSeverity pSeverity = EErrorSeverity.MINOR)
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            var type = method.DeclaringType;
            var name = method.Name;

            LogEntry.Create(type + "." + name + "\t[" + pSeverity + "]\t" + pMessage, pSeverity.GetAttribute<ErrorSeverityAttribute>().LogType);

            if (pSeverity != EErrorSeverity.MINOR)
            {
                UnityEngine.Debug.Break();
            }
        }

        public static void Create(Exception pMessage, EErrorSeverity pSeverity = EErrorSeverity.MINOR)
        {
            Create(pMessage.Message, pSeverity);
        }
    }
}
