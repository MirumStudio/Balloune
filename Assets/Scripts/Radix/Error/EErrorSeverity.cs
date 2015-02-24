using Radix.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.Error
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
        internal ErrorSeverityAttribute(ELogType aLogType)
        {
            LogType = aLogType;
        }
        public ELogType LogType { get; private set; }
    }
}
