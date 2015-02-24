﻿using System;
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
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
