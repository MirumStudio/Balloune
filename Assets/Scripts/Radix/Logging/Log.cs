using Radix.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Radix.Logging
{
    public class Log
    {
        public static void Create(string pMessage)
        {
            Create(pMessage, ELogType.INFO);
        }

        public static void Create(string pMessage, ELogType pType)
        {
            if (UnityEngine.Debug.isDebugBuild && !string.IsNullOrEmpty(pMessage))
            {
                var creator = new LogCreator();
                var entry = creator.Create(pMessage, pType);
                //UnityEngine.Debug.Log("<color=" + color + ">[" + pType + "]\t" + entry.Message + "</color>");
                ServiceManager.Instance.GetService<LogService>().AddLogEntry(entry);
            }
        }
    }

    internal class LogCreator
    {
        string[] mStackTrace;
        LogEntry mEntry;
        string mLastEntry;

        internal LogEntry Create(string pMessage, ELogType pType)
        {
            mEntry = new LogEntry()
            {
                LogType = pType,
                Message = pMessage,
                Time = DateTime.Now
            };

             HandleStackTrace();

            return mEntry;
        }
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
        private void HandleStackTrace()
        {
            mEntry.StackTrace = new string[0];
            mEntry.CallerName = "Win8 Error";
            mEntry.MemberName = "Win8 Error";
            mEntry.LineNumber = 0;
        }
#else
        private void HandleStackTrace()
        {
            ExtractStackTrace();

            if(mStackTrace.Length > 0)
            {
                mLastEntry = mStackTrace[mStackTrace.Length - 1];
                mEntry.StackTrace = mStackTrace;
                mEntry.CallerName = ExtractClassName();
                mEntry.MemberName = ExtractMethodName();
                mEntry.LineNumber = ExtractLine();
            }
        }

        private void ExtractStackTrace()
        {
            string stackTrace = UnityEngine.StackTraceUtility.ExtractStackTrace();
            string[] arg = new string[1] {"\n"};
            mStackTrace = stackTrace.Split(arg, StringSplitOptions.RemoveEmptyEntries);
        }

        private string ExtractClassName()
        {
            string className = mLastEntry.Substring(0, mLastEntry.IndexOf(":"));
            className = className.Substring(className.LastIndexOf(".") + 1);
            mLastEntry = mLastEntry.Remove(0, mLastEntry.IndexOf(":") + 1);

            return className;
        }

        private string ExtractMethodName()
        {
            string method = mLastEntry.Substring(0, mLastEntry.IndexOf(")") + 1);
            mLastEntry = mLastEntry.Remove(0, mLastEntry.IndexOf(")") + 1);

            return method;
        }

        private int ExtractLine()
        {
            string line = mLastEntry.Substring(mLastEntry.IndexOf(":") + 1);
            line = line.Remove(line.Length - 1);

            return Int32.Parse(line);
        }
#endif
    }
}
