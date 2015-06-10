/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using System;
using System.Linq;
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
using System.Reflection;
#endif
namespace Radix.Event
{
    public class EventHandlerAttribute : Attribute
    {
        public EventHandlerAttribute()
        {
            Handler = typeof(VoidDelegate);
        }

        public EventHandlerAttribute(Type pHandler)
        {
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            if (pHandler.GetTypeInfo().IsSubclassOf(typeof(Delegate)))
#else
            if (pHandler.IsSubclassOf(typeof(Delegate)))
#endif
            {
                Handler = pHandler;
            }
            else
            {
                Error.Create("Event attribute is not a delegate", EErrorSeverity.MAJOR);
            }
        }

        public Type Handler { get; private set; }
    }
}
