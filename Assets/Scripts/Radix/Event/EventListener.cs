using Radix.ErrorMangement;
using System;

namespace Radix.Event
{
    public class EventListener
    {
        public static void Register<T>(Enum _event, SingleParamEventReceiverHandler<T> _callback)
        {
            Assert.CheckNull(_callback);
            RegisterInternal(_event, _callback);
        }

        public static void Register(Enum _event, SingleParamEventReceiverHandler _callback)
        {
            Assert.CheckNull(_callback);
            RegisterInternal(_event, _callback);
        }

		public static void Register(Enum _event, TwoParamEventReceiverHandler _callback)
		{
			Assert.CheckNull(_callback);
			RegisterInternal(_event, _callback);
		}

        private static void RegisterInternal(Enum _event, Delegate _callback)
        {
            EventListener eventListener = new EventListener();

            eventListener.Event = _event;
            eventListener.Listener = _callback.Target.GetType();
            eventListener.Callback = _callback;

            EventService.RegisterEventListener(eventListener);
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

        public bool Equals(EventListener _otherListener)
        {
            return Event == _otherListener.Event &&
                   Listener == _otherListener.Listener &&
                   Callback == _otherListener.Callback;
        }
    }
}
