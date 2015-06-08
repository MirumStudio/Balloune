using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.Utlities
{
    public static class EnumUtility
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
        where TAttribute : Attribute
        {
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            return null;
#else
            var type = value.GetType();
            var name = Enum.GetName(type, value);
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
