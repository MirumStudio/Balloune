/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;

namespace Radix.Logging
{
    internal class LogEntry
    {
        internal ELogType LogType { get; set; }

        internal ELogCategory Category { get; set; }

        internal string Message { get; set; }

        internal string MemberName { get; set; }

        internal string CallerName { get; set; }

        internal int LineNumber { get; set; }

        internal DateTime Time { get; set; }

        internal string[] StackTrace { get; set; }

        internal LogEntry(){}
    }
}
