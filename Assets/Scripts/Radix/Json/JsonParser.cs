using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace Radix.Json
{
    public abstract class JsonParser<T>
    {
        protected JSONClass mJsonClass;

        public JsonParser()
        {
            mJsonClass = new JSONClass();
        }

        public abstract string Parse(T pArg);
    }
}
