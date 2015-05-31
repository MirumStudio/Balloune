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

    private Dictionary<TouchPhase, TouchHandler> mTouchPhaseHandlers;

    protected TouchControl mTouchControl;

    protected virtual void Start()
    {
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
        }
        catch(Exception ex)
        {

        }
    }

    protected virtual void HandleTouch()
    {
        foreach(Touch touch in Input.touches)
        {
            mTouchPhaseHandlers[touch.phase](touch);
        }
    }

    protected abstract void OnTouchBegan(Touch pTouch);
    protected abstract void OnTouchMoved(Touch pTouch);
    protected abstract void OnTouchStationary(Touch pTouch);
    protected abstract void OnTouchEnded(Touch pTouch);
    protected abstract void OnTouchCanceled(Touch pTouch);
}

