/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Logging;
using System;

#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
using System.Reflection;
#endif

namespace Radix.Event
{
    public class EventListener
    {
        public static void Register(Enum pEvent, SingleParamEventReceiverHandler pCallback)
        {
            Register<SingleParamEventReceiverHandler>(pEvent, pCallback);
        }

        public static void Register(Enum pEvent, TwoParamEventReceiverHandler pCallback)
		{
            Register<TwoParamEventReceiverHandler>(pEvent, pCallback);
		}

        public static void Register<T>(Enum pEvent, T pCallback)
        {
            Assert.CheckNull(pCallback);
#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(Delegate)))
#else
            if (typeof(T).IsSubclassOf(typeof(Delegate)))
#endif
            {
                RegisterInternal(pEvent, pCallback as Delegate);
            }
            else
            {
                Error.Create("Callback is not a Delegate", EErrorSeverity.MAJOR);
            }
        }

        private static void RegisterInternal(Enum pEvent, Delegate pCallback)
        {
            EventListener eventListener = new EventListener();
            
            eventListener.Event = pEvent;
            eventListener.ListenerType = pCallback.Target.GetType();
            eventListener.ListenerHashCode = pCallback.Target.GetHashCode();
            eventListener.Callback = pCallback;

            EventService.RegisterEventListener(eventListener);

            Log.Create(pCallback.Target.GetType() + " is listening " + pEvent);
        }

        public void Dispose()
        {
            Event = null;
            ListenerType = null;
            ListenerHashCode = -1;
            Callback = null;
        }

        public Enum Event
        {
            get;
            private set;
        }

        public Type ListenerType
        {
            get;
            private set;
        }

        public int ListenerHashCode
        {
            get;
            private set;
        }

        public Delegate Callback
        {
            get;
            private set;
        }

        public bool Equals(EventListener pOtherListener)
        {
            return Event == pOtherListener.Event &&
                   ListenerHashCode == pOtherListener.ListenerHashCode &&
                   Callback == pOtherListener.Callback;
        }
    }
}
