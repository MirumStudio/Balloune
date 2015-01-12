using Radix.Service;
using Radix.Utilities;
using System;

namespace Radix.Event
{
    public delegate void EventReceiverHandler(Enum _event, object _args);
    public delegate void EventReceiverHandler<T>(Enum _event, T _args);
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

        static public void UnregisterAllEventListener(Type _listenerParent)
        {
            instance.mInternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
            instance.mExternalEventDispatcher.UnregisterAllEventsListeners(_listenerParent);
        }

        static public void DipatchEvent(Enum _event, object _args = null, Type _listernerType = null)
        {
            if (IsInternalEvent(_event))
            {
                instance.mInternalEventDispatcher.DispatchEvent(_event, _args, _listernerType);
            }
            else
            {
                instance.mExternalEventDispatcher.DispatchEvent(_event, _args, _listernerType);
            }
        }

        static private bool IsInternalEvent(Enum _event)
        {
            return TypeUtility.IsInNamespace(_event.GetType(), "InternalEvent");
        }
    }
}
