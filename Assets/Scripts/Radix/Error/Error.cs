using Radix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radix.Utlities;

namespace Radix.Error
{
    public class Error
    {
        private Error() { }

        public static void Create(string pMessage, EErrorSeverity pSeverity = EErrorSeverity.MINOR)
        {
            LogEntry.Create("[" + pSeverity + "]\t" + pMessage, pSeverity.GetAttribute<ErrorSeverityAttribute>().LogType);
            UnityEngine.Debug.Break();
        }

        public static void Create(Exception pMessage, EErrorSeverity pSeverity = EErrorSeverity.MINOR)
        {
            Create(pMessage.Message, pSeverity);
        }
    }
}
