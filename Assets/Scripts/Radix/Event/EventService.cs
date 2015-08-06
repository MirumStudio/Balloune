/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Logging;
using Radix.Service;
using Radix.Utilities;
using System;
using UnityEngine;


#if UNITY_WSA || UNITY_WP8 || UNITY_WP8_1
using System.Reflection;
#endif

namespace Radix.Event
{
    public delegate void SingleParamEventReceiverHandler(Enum pEvent, object pArgs);
    public delegate void TwoParamEventReceiverHandler(Enum pEvent, object pArgs1, object pArgs2);
    public class EventService : ServiceBase
    {
        private static EventService instance = null;

        private EventDispatcher mEventDispatcher = new EventDispatcher();

        #region Init
        protected override void Init()
        {
            instance = this;
        }
        #endregion

        #region Dispose
        protected override void Dispose()
        {
            instance = null;
        }
        #endregion

        #region Register
        public static void Register(Enum pEvent, VoidDelegate pCallback)
        {
            Register<VoidDelegate>(pEvent, pCallback);
        }

        public static void Register<T>(Enum pEvent, T pCallback)
        {
            Assert.CheckNull(pCallback);
            if (IsGoodDelegateType(pEvent, typeof(T)))
            {
                instance.RegisterInternal(pEvent, pCallback as Delegate);
            }
            else
            {
                Error.Create("Callback is not a Delegate of the good type", EErrorSeverity.MAJOR);
            }
        }

        private void RegisterInternal(Enum pEvent, Delegate pCallback)
        {
            var listener = new EventListener(pEvent, pCallback as Delegate);
            mEventDispatcher.RegisterEventListener(listener);

            Log.Info(pCallback.Target.GetType() + " is listening " + pEvent, ELogCategory.RADIX);
        }
        #endregion

        #region Unregister
        static public void UnregisterAllEventListener(Type pEvent)
        {
            instance.mEventDispatcher.UnregisterAllEventsListeners(pEvent);
        }

        static public void UnregisterEventListener(Enum pEvent, System.Object pListenerParent)
        {
            instance.mEventDispatcher.UnregisterEventListener(pEvent, pListenerParent);
        }
        #endregion

        #region Dispatch
        static public void DispatchEvent(Enum pEvent, params object[] pArgs)
        {
            Log.Info("Dispatch event : " + pEvent, ELogCategory.RADIX);

            instance.mEventDispatcher.DispatchEvent(pEvent, pArgs);
        }
        #endregion

        #region Utility
        private static bool IsGoodDelegateType(Enum pEvent, Type pType)
        {
            Type type = null;
            try
            {
                type = pEvent.GetAttribute<EventHandlerAttribute>().Handler;
            }
            catch (Exception ex)
            {
                Error.Create(ex.Message, EErrorSeverity.MAJOR);
            }

            return type!= null && type == pType;
        }
        #endregion
    }
}
