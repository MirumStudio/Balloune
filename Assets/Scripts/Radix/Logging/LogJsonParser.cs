using Radix.Json;
using Radix.Logging;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.Logging
{
    internal class LogJsonParser : JsonParser<LogFile>
    {

        public override string Parse(LogFile pArg)
        {
            JSONArray array = new JSONArray();

            foreach(LogEntry entry in pArg.LogEntries)
            {
                JSONClass data = new JSONClass();

                data.Add("Type", new JSONData((int)entry.LogType));
                data.Add("Category", new JSONData((int)entry.Category));
                data.Add("Message", new JSONData(entry.Message));
                data.Add("Time", new JSONData(entry.Time.ToString()));
                data.Add("MemberName", new JSONData(entry.MemberName));
                data.Add("CallerName", new JSONData(entry.CallerName));
                data.Add("LineNumber", new JSONData(entry.LineNumber));

                JSONArray stack = new JSONArray();
                foreach(string str in entry.StackTrace)
                {
                    stack.Add(str);
                }

                data.Add("StackTrace", stack);

                array.Add(data);
            }

            mJsonClass.Add("Logs", array);

            return mJsonClass.ToJSON(0);
        }
    }
}
