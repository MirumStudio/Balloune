using System;
using System.Runtime.Serialization;

namespace Radix.Logging
{
    public enum ELogType
    {
        [LogTypeAttribute(ELogPriority.MINOR)]
        INFO,

        [LogTypeAttribute(ELogPriority.MINOR)]
        DEBUG,

        [LogTypeAttribute(ELogPriority.MAJOR)]
        WARNING,

        [LogTypeAttribute(ELogPriority.CRITICAL)]
        ERROR,
    }

    public class LogTypeAttribute : Attribute
    {
        internal LogTypeAttribute(ELogPriority aPriority)
        {
            Priority = aPriority;
        }
        public ELogPriority Priority { get; private set; }
    }
}
