using System;

namespace Radix.Service
{
    [ServiceType(EServiceType.STANDARD, EServiceType.DEBUG)] 
    public abstract class ServiceBase : IService
    {
        public EServiceState State
        {
            get;
            private set;
        }

        internal ServiceBase()
        {
            State = EServiceState.DISPOSED;
        }

        protected abstract void Init();
        protected abstract void Dispose();

        protected virtual void Start() { }
        protected virtual void Stop() { }
        protected virtual void Restart() { }

        internal void CallInit()
        {
            if (StateIs(EServiceState.DISPOSED))
            {
                this.Init();
                State = EServiceState.IDLE;
            }
            else
            {
                throw new InvalidOperationException("Invalide Service State");
            }
        }

        internal void CallDispose()
        {
            if (!StateIs(EServiceState.DISPOSED))
            {
                this.Dispose();
                State = EServiceState.DISPOSED;
            }
            else
            {
                throw new InvalidOperationException("Invalide Service State");
            }
        }

        internal void CallStart()
        {
            if (StateIs(EServiceState.IDLE) || StateIs(EServiceState.STOPPED))
            {
                this.Start();
                State = EServiceState.STARTED;
            }
            else
            {
                throw new InvalidOperationException("Invalide Service State");
            }
        }

        internal void CallStop()
        {
            if (StateIs(EServiceState.STARTED))
            {
                this.Stop();
                State = EServiceState.STOPPED;
            }
            else
            {
                throw new InvalidOperationException("Invalide Service State");
            }
        }

        internal void CallRestart()
        {
            if (StateIs(EServiceState.FAULTED))
            {
                this.Dispose();
                this.Init();
                this.Start();
            }
            else
            {
                throw new InvalidOperationException("Invalide Service State");
            }
        }

        protected bool StateIs(EServiceState _state)
        {
            return State == _state;
        }
    }
}
