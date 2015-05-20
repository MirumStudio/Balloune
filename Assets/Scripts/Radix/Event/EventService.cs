using Radix.Service;
using Radix.Utilities;
using System;
using UnityEngine;

namespace Radix.Event
{
	public delegate void SingleParamEventReceiverHandler(Enum _event, object _args);
	public delegate void SingleParamEventReceiverHandler<T>(Enum _event, T _args);
	public delegate void TwoParamEventReceiverHandler(Enum _event, object _arg1, object _arg2);
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

        static internal void RegisterEventListener(EventListener _listener)
        {
            if (IsInternalEvent(_listener.Event))
            {
                instance.mInternalEventDispatcher.RegisterEventListener(_listener);
            }
            else
            {
                instance.mExternalEventDispatcher.RegisterEventListener(_listener);
            }
        }

        /*static public void UnregisterAllEventListener(Type _listenerParent)
        {
            instance.mInternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
            instance.mExternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
        }*/

        static public void UnregisterAllEventListener(Type _Event)
        {
            instance.mInternalEventDispatcher.UnregisterAllEventsListeners(_Event);
            instance.mExternalEventDispatcher.UnregisterAllEventsListeners(_Event);
        }

        static public void DispatchEvent(Enum _event, object _args /*= null*/)
        {
            Debug.Log("Dispatch event : " + _event);
            if (IsInternalEvent(_event))
            {
                instance.mInternalEventDispatcher.DispatchEvent(_event, _args, null);
            }
            else
            {
                instance.mExternalEventDispatcher.DispatchEvent(_event, _args, null);
            }
        }

		static public void DispatchEvent(Enum _event, object _arg1, object _arg2 /*= null*/)
		{
			Debug.Log("Dispatch event : " + _event);
			if (IsInternalEvent(_event))
			{
				instance.mInternalEventDispatcher.DispatchEvent(_event, _arg1, _arg2, null);
			}
			else
			{
				instance.mExternalEventDispatcher.DispatchEvent(_event, _arg1, _arg2, null);
			}
		}

        static private bool IsInternalEvent(Enum _event)
        {
            return false;
            //return TypeUtility.IsInNamespace(_event.GetType(), "InternalEvent");
        }
    }
}
