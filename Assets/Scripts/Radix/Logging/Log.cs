/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Service;
using Radix.Utilities;
using System;

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
            Create(pMessage, pType, ELogCategory.NONE);
        }

        public static void Create(string pMessage, ELogCategory pCategory)
        {
            Create(pMessage, ELogType.INFO, pCategory);
        }

        public static void Create(string pMessage, ELogType pType, ELogCategory pCategory)
        {
            if (UnityEngine.Debug.isDebugBuild && !string.IsNullOrEmpty(pMessage))
            {
                var creator = new LogCreator();
                var entry = creator.Create(pMessage, pType, pCategory);

                ShowLog(entry);

                ServiceManager.Instance.GetService<LogService>().AddLogEntry(entry);
            }
        }

        private static void ShowLog(LogEntry pEntry)
        {

            if ((int)pEntry.LogType <= LogConfig.UNITY_EDITOR_CONSOLE_LOG_LEVEL)
            {
                ShowLogEditorConsole(pEntry);
            }

            if ((int)pEntry.LogType <= LogConfig.IN_GAME_CONSOLE_LOG_LEVEL)
            {
                ShowLogDebugText(pEntry);
            }
        }

        private static void ShowLogEditorConsole(LogEntry pEntry)
        {
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            UnityEngine.Debug.Log("<color=black>[" + pEntry.LogType + "]\t" + pEntry.Message + "</color>");
#else
            UnityEngine.Debug.Log("<color=" + pEntry.LogType.GetAttribute<LogTypeAttribute>().Color + ">[" + pEntry.LogType + "]\t" + pEntry.Message + "</color>");
#endif
        }


        private static void ShowLogDebugText(LogEntry pEntry)
        {
            DebugText.Log(pEntry.Message);
        }
    }
}
