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

            if (!mEventDictionnary.ContainsKey(pListener.Event))
            {
                mEventDictionnary[pListener.Event] = new List<EventListener>();
            }

            if (!ContainListener(pListener))
            {
                mEventDictionnary[pListener.Event].Add(pListener);
            }
        }
        #endregion

        #region Unregister
        private void UnregisterEventListener(EventListener pListener, IList<EventListener> pList)
        {
            if (pListener != null)
            {
                pListener.Dispose();
                pList.Remove(pListener);
                pListener = null;
            }
        }

        internal void UnregisterEventListener(Enum pEvent, Object pListenerParent)
        {
            if (mEventDictionnary.ContainsKey(pEvent))
            {
                EventListener eventListener = mEventDictionnary[pEvent].FirstOrDefault((currentEventListener) => { return currentEventListener.ListenerHashCode == pListenerParent.GetHashCode(); });
                UnregisterEventListener(eventListener, mEventDictionnary[pEvent]);
            }
        }

        internal void UnregisterAllEventsListeners(Type pEvent)
        {
            foreach (EventPair eventPair in mEventDictionnary)
            {
                EventListener eventListener = eventPair.Value.FirstOrDefault((currentEventListener) => 
                {
                    return currentEventListener.Event.GetType() == pEvent; 
                });
                UnregisterEventListener(eventListener, eventPair.Value);
            }
        }
        #endregion

        #region Dispatch
        internal void DispatchEvent(Enum pEvent, Type _listenerType, params object[] pArgs)
        {
            if (mEventDictionnary.ContainsKey(pEvent))
            {

                foreach (EventListener listener in mEventDictionnary[pEvent])
                {
                    if (_listenerType == null || _listenerType == listener.ListenerType)
                    {
                        Assert.CheckNull(listener.Callback);
                        listener.Callback.DynamicInvoke(pArgs);
                    }
                }
            }
        }

        internal void DispatchEvent(Enum pEvent, params object[] pArgs)
        {
            DispatchEvent(pEvent, null, pArgs);
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
