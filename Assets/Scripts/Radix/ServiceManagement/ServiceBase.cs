/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
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
                TrowInvalidStateError();
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
                TrowInvalidStateError();
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
                TrowInvalidStateError();
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
                TrowInvalidStateError();
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
                TrowInvalidStateError();
            }
        }

        protected bool StateIs(EServiceState pState)
        {
            return State == pState;
        }

        private void TrowInvalidStateError()
        {
            Error.Create("Invalid Service State", EErrorSeverity.CRITICAL);
        }
    }
}
