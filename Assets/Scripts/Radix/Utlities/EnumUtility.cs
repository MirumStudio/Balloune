/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;
using System.Linq;

#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
using System.Reflection;
#endif

namespace Radix.Utlities
{
    public static class EnumUtility
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum pValue)
        where TAttribute : Attribute
        {
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            var type = pValue.GetType();
            var name = Enum.GetName(type, pValue);
            return type.GetTypeInfo().GetDeclaredField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
#else
            var type = pValue.GetType();
            var name = Enum.GetName(type, pValue);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();

#endif
        }

        public static T ObjectToEnum<T>(object o)
        {
            T enumVal = (T)Enum.Parse(typeof(T), o.ToString());
            return enumVal;
        }
    }
}
