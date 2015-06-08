using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Radix.Logging
{
    public enum ELogType
    {
        [LogTypeAttribute(ELogPriority.CRITICAL, "red")]
        ERROR = 0,

        [LogTypeAttribute(ELogPriority.MAJOR, "yellow")]
        WARNING = 1,

        [LogTypeAttribute(ELogPriority.MINOR, "blue")]
        DEBUG = 2,

        [LogTypeAttribute(ELogPriority.MINOR)]
        INFO = 3,
    }

    internal class LogTypeAttribute : Attribute
    {
        internal LogTypeAttribute(ELogPriority pPriority)
        {
            Priority = pPriority;
            Color = "black";
        }

        internal LogTypeAttribute(ELogPriority pPriority, string pColor)
        {
            Priority = pPriority;
            Color = pColor;
        }
        internal ELogPriority Priority { get; private set; }
        internal string Color { get; private set; }
    }
}
