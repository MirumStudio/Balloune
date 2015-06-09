/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

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
