/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Logging;
using Radix.Service;
using Radix.Utilities;
using System;
using UnityEngine;

namespace Radix.Event
{
    public delegate void SingleParamEventReceiverHandler(Enum pEvent, object pArgs);
    public delegate void TwoParamEventReceiverHandler(Enum pEvent, object pArgs1, object pArgs2);
    public class EventService : ServiceBase
    {
        private static EventService instance = null;

        protected override void Init()
        {
            instance = this;
        }

        protected override void Dispose()
        {
            instance = null;
        }

        private EventDispatcher mInternalEventDispatcher = new EventDispatcher();
        private EventDispatcher mExternalEventDispatcher = new EventDispatcher();

        static internal void RegisterEventListener(EventListener pListener)
        {
            if (IsInternalEvent(pListener.Event))
            {
                instance.mInternalEventDispatcher.RegisterEventListener(pListener);
            }
            else
            {
                instance.mExternalEventDispatcher.RegisterEventListener(pListener);
            }
        }

        /*static public void UnregisterAllEventListener(Type _listenerParent)
        {
            instance.mInternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
            instance.mExternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
        }*/

        static public void UnregisterAllEventListener(Type pEvent)
        {
            instance.mInternalEventDispatcher.UnregisterAllEventsListeners(pEvent);
            instance.mExternalEventDispatcher.UnregisterAllEventsListeners(pEvent);
        }

        static public void DispatchEvent(Enum pEvent, params object[] pArgs)
        {
            Log.Create("Dispatch event : " + pEvent);
            if (IsInternalEvent(pEvent))
            {
                instance.mInternalEventDispatcher.DispatchEvent(pEvent, pArgs);
            }
            else
            {
                instance.mExternalEventDispatcher.DispatchEvent(pEvent, pArgs);
            }
        }
        static private bool IsInternalEvent(Enum pEvent)
        {
            return false;
            //return TypeUtility.IsInNamespace(_event.GetType(), "InternalEvent");
        }

        public static void Register<T>(Enum pEvent, T pCallback)
        {
            EventListener.Register<T>(pEvent, pCallback);
        }

        public static void Register(Enum pEvent, VoidDelegate pCallback)
        {
            EventListener.Register<VoidDelegate>(pEvent, pCallback);
        }
    }
}
