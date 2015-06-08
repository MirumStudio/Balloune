using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using Radix.Service;


namespace Radix.Logging
{
   // [DataContract]
    internal class LogEntry
    {
        //[DataMember]
        internal ELogType LogType { get; set; }

        //[DataMember]
        internal string Message { get; set; }

        //[DataMember]
        internal string MemberName { get; set; }

        //[DataMember]
        internal string CallerName { get; set; }

        //[DataMember]
        internal int LineNumber { get; set; }

        //[DataMember]
        internal DateTime Time { get; set; }

        //[DataMember]
        internal string[] StackTrace { get; set; }

        internal LogEntry(){}
    }
}
