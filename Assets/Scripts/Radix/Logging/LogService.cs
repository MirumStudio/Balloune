/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Service;

namespace Radix.Logging
{
    internal class LogService : ServiceBase
    {
        private LogFile mLogFile;

        protected override void Init()
        {
            mLogFile = new LogFile();
        }

        protected override void Dispose()
        {}

        internal void AddLogEntry(LogEntry aLogEntry)
        {
            mLogFile.AddLogEntry(aLogEntry);
            if (LogConfig.SAVE_LOG_TO_FILE)
            {
                mLogFile.Save();
            }
        }
    }
}
