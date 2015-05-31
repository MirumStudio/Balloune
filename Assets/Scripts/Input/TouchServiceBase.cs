using Radix.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public abstract class TouchServiceBase : MonoBehaviour
{
    private delegate void TouchHandler(Touch touch);
  /*  protected override void Init()
    {
        //throw new NotImplementedException();
    }

    protected override void Dispose()
    {
        //throw new NotImplementedException();
    }*/

    int mTouchCount = 0;

    Dictionary<TouchPhase, TouchHandler> mTouchPhaseHandlers;

    TouchControl mtouh;

    protected virtual void Start()
    {

        mtouh = new TouchControl();
        mTouchPhaseHandlers = new Dictionary<TouchPhase,TouchHandler>();
        mTouchPhaseHandlers.Add(TouchPhase.Began, OnTouchBegan);
        mTouchPhaseHandlers.Add(TouchPhase.Moved, OnTouchMoved);
        mTouchPhaseHandlers.Add(TouchPhase.Stationary, OnTouchStationary);
        mTouchPhaseHandlers.Add(TouchPhase.Ended, OnTouchEnded);
        mTouchPhaseHandlers.Add(TouchPhase.Canceled, OnTouchCanceled);
    }

    protected virtual void Update()
    {
        try
        {
            HandleTouch();
            mtouh.info();
        }
        catch(Exception ex)
        {

        }
    }

    protected virtual void HandleTouch()
    {
        //Debug.Log("----------------------------------------");
        foreach(Touch touch in Input.touches)
        {
            mtouh.mTouch = touch;
            mTouchPhaseHandlers[touch.phase](touch);
            
        }
    }

    protected abstract void OnTouchBegan(Touch pTouch);
    protected abstract void OnTouchMoved(Touch pTouch);
    protected abstract void OnTouchStationary(Touch pTouch);
    protected abstract void OnTouchEnded(Touch pTouch);
    protected abstract void OnTouchCanceled(Touch pTouch);
}

