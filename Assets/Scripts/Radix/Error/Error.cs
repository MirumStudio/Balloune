using Radix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Radix.Utlities;
using System.Diagnostics;

namespace Radix.ErrorMangement
{
    public class Error
    {
        private Error() { }

        public static void Create(string pMessage)
        {
            Create(pMessage, EErrorSeverity.MINOR);
        }

        public static void Create(string pMessage, EErrorSeverity pSeverity)
        {
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            Log.Create(pMessage, ELogType.ERROR);
#else
            Log.Create(pMessage, pSeverity.GetAttribute<ErrorSeverityAttribute>().LogType);
            
#endif

            if (pSeverity != EErrorSeverity.MINOR)
            {
                UnityEngine.Debug.Break();
            }
        }

        public static void Create(Exception pMessage)
        {
            Create(pMessage, EErrorSeverity.CRITICAL);
        }

        public static void Create(Exception pMessage, EErrorSeverity pSeverity)
        {
            Create(pMessage.Message, pSeverity);
        }
    }
}
