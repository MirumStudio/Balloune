/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using System;
using System.Collections.Generic;
using System.Linq;
// Define Complex Type
using EventDictionnary = System.Collections.Generic.Dictionary<System.Enum, System.Collections.Generic.IList<Radix.Event.EventListener>>;
using EventPair = System.Collections.Generic.KeyValuePair<System.Enum, System.Collections.Generic.IList<Radix.Event.EventListener>>;


namespace Radix.Event
{
    internal class EventDispatcher
    {
        private static EventDictionnary mEventDictionnary = new EventDictionnary();

        #region Register
        internal void RegisterEventListener(EventListener pListener)
        {
            Assert.CheckNull(pListener);

            CheckIfEventExist(pListener.Event);
            AddListener(pListener);
        }

        private void CheckIfEventExist(Enum pEvent)
        {
            if (!mEventDictionnary.ContainsKey(pEvent))
            {
                mEventDictionnary[pEvent] = new List<EventListener>();
            }
        }

        private void AddListener(EventListener pListener)
        {
            if (!ContainListener(pListener))
            {
                mEventDictionnary[pListener.Event].Add(pListener);
            }
        }

        #endregion

        #region Unregister
        private void UnregisterEventListener(EventListener pListener)
        {
            if (pListener != null)
            {
                pListener.Dispose();
                pListener = null;
            }
        }

        internal void UnregisterEventListener(Enum pEvent, Object pListenerParent)
        {
            if (mEventDictionnary.ContainsKey(pEvent))
            {
                EventListener eventListener = mEventDictionnary[pEvent].FirstOrDefault((currentEventListener) => { return currentEventListener.ListenerHashCode == pListenerParent.GetHashCode(); });
                mEventDictionnary[pEvent].Remove(eventListener);
                UnregisterEventListener(eventListener);
            }
        }

        internal void UnregisterAllEventsListeners(Type pEvent)
        {
            List<Enum> eventToRemove = new List<Enum>();
            foreach (EventPair eventPair in mEventDictionnary)
            {
                if(eventPair.Key.GetType() == pEvent)
                {
                    foreach(EventListener listener in eventPair.Value)
                    {
                        UnregisterEventListener(listener);
                    }
                    eventPair.Value.Clear();
                    eventToRemove.Add(eventPair.Key);
                }
            }
        }

        private void RemoveEvent(List<Enum> pEventToRemove)
        {
            foreach (Enum eventName in pEventToRemove)
            {
                if (mEventDictionnary.ContainsKey(eventName) 
                    && mEventDictionnary[eventName].Count == 0)
                {
                    mEventDictionnary.Remove(eventName);
                }
            }
        }

        #endregion

        #region Dispatch
        internal void DispatchEvent(Enum pEvent, params object[] pArgs)
        {
            DispatchEvent(pEvent, null, pArgs);
        }

        internal void DispatchEvent(Enum pEvent, Type _listenerType, params object[] pArgs)
        {
            if (mEventDictionnary.ContainsKey(pEvent))
            {
                foreach (EventListener listener in mEventDictionnary[pEvent])
                {
                    if (_listenerType == null || _listenerType == listener.ListenerType)
                    {
                        Assert.CheckNull(listener.Callback);
                        InvokeCallback(listener.Callback, pArgs);
                    }
                }
            }
        }

        private void InvokeCallback(Delegate pCallback, params object[] pArgs)
        {  
			try
            {
				pCallback.DynamicInvoke(pArgs);
            }
            catch(Exception ex)
            {
                Error.Create("Error when attempting to invoke callback " + pCallback + " with args " + pArgs[0] + ", error is : " + ex.Message, EErrorSeverity.CRITICAL);
            }
        }
        #endregion

        #region Utility
        private bool ContainListener(EventListener pListener)
        {
            Assert.CheckNull(pListener);
            return (mEventDictionnary[pListener.Event].FirstOrDefault((eventListener) => { return eventListener.Equals(pListener); }) != null);
        }
        #endregion
    }
}
