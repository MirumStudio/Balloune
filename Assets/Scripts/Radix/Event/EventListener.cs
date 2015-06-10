/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;
using Radix.Utilities;
namespace Radix.Event
{
    internal class EventListener
    {
        internal EventListener(Enum pEvent, Delegate pCallback)
        {
            Event = pEvent;
            ListenerType = pCallback.Target.GetType();
            ListenerHashCode = pCallback.Target.GetHashCode();
            Callback = pCallback;
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
