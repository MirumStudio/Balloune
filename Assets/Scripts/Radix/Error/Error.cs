/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Logging;
using Radix.Utlities;
using System;

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
