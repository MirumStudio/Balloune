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
    public class LogEntry
    {
        //[DataMember]
        public ELogType LogType { get; set; }

        //[DataMember]
        public string Message { get; set; }

        //[DataMember]
        public string MemberName { get; set; }

        //[DataMember]
        public string CallerName { get; set; }

        //[DataMember]
        public int LineNumber { get; set; }

        //[DataMember]
        public DateTime Time { get; set; }

        //[DataMember]
        public string StackTrace { get; set; }

        private LogEntry(){}

        static public void Create(string aMessage, ELogType pType/* = ELogType.INFO*/)
        {
            string color;
            switch(pType)
            {
                case ELogType.DEBUG:
                    {
                        color = "green";
                        break;
                    }
                case ELogType.INFO:
                    {
                        color = "blue";
                        break;
                    }
                case ELogType.WARNING:
                    {
                        color = "brown";
                        break;
                    }
                case ELogType.ERROR:
                    {
                        color = "red";
                        break;
                    }
                default:
                    {
                        color = "black";
                        break;
                    }
            }


            UnityEngine.Debug.Log("<color=" + color + ">[" + pType + "]\t" + aMessage + "</color>");
        }

        /*static public LogEntry Create(ELogType aType, string aMessage, bool aSave = true,
        [CallerMemberName] string aMemberName = "",
        [CallerFilePath] string aSourceFilePath = "",
        [CallerLineNumber] int aSourceLineNumber = 0)
        {
            /*var logEntry = new LogEntry
            {
                LogType = aType,
                Message = aMessage,
                MemberName = aMemberName,
                LineNumber = aSourceLineNumber,
                Time = DateTime.Now
            };

            logEntry.CallerName = aSourceFilePath.Substring(aSourceFilePath.LastIndexOf(@"\") + 1);

#if DEBUG
            var trace = new StackTrace();
            StackFrame[] stackFrames = trace.GetFrames();

            //Iteration is beginning at index 1 because the index 0 is this method "Create"
            for(int i = 1; i < stackFrames.Count(); i++)
            {
                logEntry.StackTrace += "\n" + stackFrames[i].GetMethod().Name;
            }
#endif

            if(aSave)
            {
                logEntry.Save();
            }

            return logEntry;
            return null;
        }*/

        public void Save()
        {
            ServiceManager.Instance.GetService<LogService>().AddLogEntry(this);
        }
    }
}
