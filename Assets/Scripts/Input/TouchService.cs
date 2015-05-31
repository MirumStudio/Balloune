using Radix.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_IOS
public class TouchService : iOSTouchService
#else
public class TouchService : DefaultTouchService
#endif
{

    #region TouchBegan
    protected override void OnTouchBegan(Touch pTouch)
    {
        if(mTouchControl == null)
        {
            mTouchControl = new TouchControl(pTouch);

            HandlerNewTouch();
        }
    }

    private void HandlerNewTouch()
    {
        if (mTouchControl.IsDoubleTap)
        {
            DispatchDoubleTapEvent();
        }
        else
        {
            DispatchTapEvent();
        }
    }

    private void DispatchTapEvent()
    {
        EventService.DispatchEvent(ETouchEvent.TAP, mTouchControl.Position);
    }

    private void DispatchDoubleTapEvent()
    {
        EventService.DispatchEvent(ETouchEvent.DOUBLE_TAP, mTouchControl.Position);
    }

    #endregion

    #region TouchMoved

    protected override void OnTouchMoved(Touch pTouch)
    {
        if(IsCurrentTouch(pTouch))
        {
            mTouchControl.UpdateTouch(pTouch);

            bool oldSwipe = mTouchControl.IsSwiping;

            mTouchControl.Moved();

            if(!oldSwipe && mTouchControl.IsSwiping)
            {
                DispatchSwipeBeginEvent();
            }
        }
        //swipe begin


        //circle draw update
    }

    private void DispatchSwipeBeginEvent()
    {
        EventService.DispatchEvent(ETouchEvent.SWIPE_BEGIN, null);
    }

    #endregion



    #region TouchStationary

    protected override void OnTouchStationary(Touch pTouch)
    {
        if (IsCurrentTouch(pTouch))
        {
            mTouchControl.UpdateTouch(pTouch);
            EndSwipe();
        }
    }

    #endregion

    #region TouchEnded

    protected override void OnTouchEnded(Touch pTouch)
    {
        //check touch null.. Dispose ???
        if (IsCurrentTouch(pTouch))
        {
            EndSwipe();
            mTouchControl = null;
            DispatchTouchEnded();
        }
        //circle drawnn
        //end swipe
    }

    private void DispatchTouchEnded()
    {
        EventService.DispatchEvent(ETouchEvent.END, null);
    }

    #endregion

    #region TouchCanceled

    protected override void OnTouchCanceled(Touch pTouch)
    {
        //Touch cancelled
    }

    #endregion

    #region Private Utilities Method

    private bool IsCurrentTouch(Touch pTouch)
    {
        return mTouchControl.ID == pTouch.fingerId;
    }

    private void UpdateCurrentTouch(Touch pTouch)
    {
        mTouchControl.UpdateTouch(pTouch);
    }

    private void EndSwipe()
    {
        if(mTouchControl.IsSwiping)
        {
            DispatchSwipeEndEvent();
            mTouchControl.EndSwipe();
        }
    }

    private void DispatchSwipeEndEvent()
    {
        EventService.DispatchEvent(ETouchEvent.SWIPE_END, mTouchControl.SwipeDistance);
    }
    #endregion
}
