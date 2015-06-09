/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Logging;
using System;

namespace Radix.Event
{
    public class EventListener
    {
        public static void Register<T>(Enum pEvent, SingleParamEventReceiverHandler<T> pCallback)
        {
            Assert.CheckNull(pCallback);
            RegisterInternal(pEvent, pCallback);
        }

        public static void Register(Enum pEvent, SingleParamEventReceiverHandler pCallback)
        {
            Assert.CheckNull(pCallback);
            RegisterInternal(pEvent, pCallback);
        }

        public static void Register(Enum pEvent, TwoParamEventReceiverHandler pCallback)
		{
            Assert.CheckNull(pCallback);
            RegisterInternal(pEvent, pCallback);
		}

        private static void RegisterInternal(Enum pEvent, Delegate pCallback)
        {
            EventListener eventListener = new EventListener();
            
            eventListener.Event = pEvent;
            eventListener.Listener = pCallback.Target.GetType();
            eventListener.Callback = pCallback;

            EventService.RegisterEventListener(eventListener);

            Log.Create(pCallback.Target.GetType() + " is listening " + pEvent);
        }

        public void Dispose()
        {
            Event = null;
            Listener = null;
            Callback = null;
        }

        public Enum Event
        {
            get;
            private set;
        }

        public Type Listener
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
                   Listener == pOtherListener.Listener &&
                   Callback == pOtherListener.Callback;
        }
    }
}
