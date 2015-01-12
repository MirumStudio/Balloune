using Radix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Radix.Logging
{
    //[DataContract]
    internal class LogFile
    {
        private string LOG_NAME = "Log_{0}h{1}_{2}.txt";

        //[DataMember]
        public IList<LogEntry> LogEntries;

        public LogFile()
        {
            LogEntries = new List<LogEntry>();
        }

        public void Save()
        {
          //  JsonUtility.SaveJsonToFile<LogFile>(this, GetLogName());
        }

        public void AddLogEntry(LogEntry aLogEntry)
        {
            LogEntries.Add(aLogEntry);
        }

        private DateTime GetFirstLogTime()
        {
            if (LogEntries != null && LogEntries.Count > 0)
            {
                return LogEntries.First().Time;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        private string GetLogName()
        {
            DateTime time = GetFirstLogTime();
            string date = time.ToString("MM-dd-yy");
            return String.Format(LOG_NAME, time.Hour, time.Minute, date);
        }
    }
}
