using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TouchControl
{
    private const float STATIONARY_AJUST_TIME = 0.02f;
    private const float STATIONARY_AJUST_TIME_DEBUG = 0.1f;
    private float mStationaryAjustTime = 0f;

    public Touch mTouch;
    private float mStationaryTime = 0f;

    public TouchControl(Touch pTouch)
    {
        mStationaryAjustTime = Application.isEditor ? STATIONARY_AJUST_TIME_DEBUG : STATIONARY_AJUST_TIME;
        UpdateTouch(pTouch);
        IsSwiping = false;
    }

    //NOTE: Multitap is considered for a double tap
    //Author: vbernier
    public bool IsDoubleTap
    {
        get { return mTouch.IsMultiTap(); }
    }

    public int ID
    {
        get { return mTouch.fingerId; }
    }

    public Vector2 Position
    {
        get { return Camera.main.ScreenToWorldPoint(mTouch.position); }
    }

    private bool mCanBeSwipe = false;

    private Vector2 mStartSwipePosition;
    private float mSwipeTime;

    //Stationary phase of unity is too sensible
    public bool IsStationary
    {
        get { return mStationaryTime > mStationaryAjustTime; }
    }

    public bool IsSwiping
    {
        private set;
        get;
    }

    public float SwipeDistance
    {
        get
        {
            return IsSwiping ? InternalSwipeDistance() : 0f;
        }
    }

    private float InternalSwipeDistance()
    {
        return Vector2.Distance(mStartSwipePosition, mTouch.position);
    }

    public void UpdateTouch(Touch pTouch)
    {
        mTouch = pTouch;
    }

    public void Moved()
    {
        if(!mCanBeSwipe)
        {
            InitSwipe();
        }
        else
        {
            UpdateSwipe();
        }
    }

    private void InitSwipe()
    {
        mCanBeSwipe = true;
        mStartSwipePosition = mTouch.position;

        mSwipeTime = 0f;
        AddSwipeTime();
    }

    private void UpdateSwipe()
    {
        AddSwipeTime();

        if (mSwipeTime > 0f && InternalSwipeDistance() > 0f)
        {
            IsSwiping = true;
        }
    }

    public void EndSwipe()
    {
        IsSwiping = false;
        mCanBeSwipe = false;
    }

    private void AddSwipeTime()
    {
        mStationaryTime = 0f;

        //DANGEROUS
        mSwipeTime += mTouch.deltaTime;
    }

    public void OnStationary()
    {
        mStationaryTime += mTouch.deltaTime;
    }
}
