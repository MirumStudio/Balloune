/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Logging;
using System;

namespace Radix.ErrorMangement
{
    public enum EErrorSeverity
    {
        [ErrorSeverityAttribute(ELogType.WARNING)]
        MINOR,

        [ErrorSeverityAttribute(ELogType.ERROR)]
        MAJOR,

        [ErrorSeverityAttribute(ELogType.ERROR)]
        CRITICAL,

        [ErrorSeverityAttribute(ELogType.ERROR)]
        ASSERT
    }

    public class ErrorSeverityAttribute : Attribute
    {
        internal ErrorSeverityAttribute(ELogType pLogType)
        {
            LogType = pLogType;
        }
        public ELogType LogType { get; private set; }
    }
}
