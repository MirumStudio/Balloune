/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using System;

namespace Radix.Logging
{
    internal class LogCreator
    {
        string[] mStackTrace;
        LogEntry mEntry;
        string mLastEntry;

        internal LogEntry Create(string pMessage, ELogType pType, ELogCategory pCategory)
        {
            mEntry = new LogEntry()
            {
                LogType = pType,
                Message = pMessage,
                Time = DateTime.Now,
                Category = pCategory
            };

            HandleStackTrace();

            return mEntry;
        }
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1 || UNITY_ANDROID
        private void HandleStackTrace()
        {
            mEntry.StackTrace = new string[0];
            mEntry.CallerName = "Error";
            mEntry.MemberName = "Error";
            mEntry.LineNumber = 0;
        }
#else
        private void HandleStackTrace()
        {
            ExtractStackTrace();

            if (mStackTrace.Length > 0)
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
            string[] arg = new string[1] { "\n" };
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
            string line = mLastEntry.Substring(mLastEntry.LastIndexOf(":") + 1);
            line = line.Remove(line.Length - 1);

            int lineNumber = 0;
            try
            {
                lineNumber = Int32.Parse(line);
            }
            catch(Exception ex)
            {
            }

            return lineNumber;
        }
#endif
    }
}
