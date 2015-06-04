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
    #region static function
    private static TouchService mInstance = null;

    public static Vector2 CurrentTouchPosition
    {
        get
        {
            if (mInstance)
            {
                return mInstance.mTouchControl.Position;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

    protected override void Start()
    {
        mInstance = this;
        base.Start();
    }
    #endregion

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
        DebugText.Log("TAP");
    }

    private void DispatchDoubleTapEvent()
    {
        EventService.DispatchEvent(ETouchEvent.DOUBLE_TAP, mTouchControl.Position);
        DebugText.Log("Double TAP");
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
    }

    private void DispatchSwipeBeginEvent()
    {
        EventService.DispatchEvent(ETouchEvent.SWIPE_BEGIN, mTouchControl.SwipeDirection);
        DebugText.Log("SWIPE_BEGIN, " + mTouchControl.SwipeDirection.ToString());
    }

    #endregion

    #region TouchStationary

    protected override void OnTouchStationary(Touch pTouch)
    {
        if (IsCurrentTouch(pTouch))
        {
            mTouchControl.UpdateTouch(pTouch);
            mTouchControl.OnStationary();
            if (mTouchControl.IsStationary)
            {
                EndSwipe();
            }
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
    }

    private void DispatchTouchEnded()
    {
        EventService.DispatchEvent(ETouchEvent.END, null);
        DebugText.Log("END");
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
        DebugText.Log("SWIPE_END, " + mTouchControl.SwipeDistance.ToString());
    }
    #endregion
}
