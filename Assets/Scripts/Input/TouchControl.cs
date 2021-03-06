﻿/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class TouchControl
{
    private const float STATIONARY_ADJUST_TIME = 0.013f;
    private const float STATIONARY_ADJUST_TIME_DEBUG = 0.1f;
    private float mStationaryAjustTime = 0f;

    private const float SWIPE_MAX_TIME = 0.1f;
    private const float SWIPE_MAX_TIME_DEBUG = 0.5f;
    private float mSwipeMaxTime = 0f;

    private const int MIN_ANGLE = 30;
    private const int MAX_ANGLE = 150;

    public Touch mTouch;
    private float mStationaryTime = 0f;

    public TouchControl(Touch pTouch)
    {
        SwipeDirection = ESwipeDirection.NONE;
        mStationaryAjustTime = Application.isEditor ? STATIONARY_ADJUST_TIME_DEBUG : STATIONARY_ADJUST_TIME;
        mSwipeMaxTime = Application.isEditor ? SWIPE_MAX_TIME_DEBUG : SWIPE_MAX_TIME;
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
    private Vector2 mLastSwipePosition;
    private float mSwipeDistance;
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

    public ESwipeDirection SwipeDirection
    {
        private set;
        get;
    }

    public float SwipeDistance
    {
        get
        {
            return IsSwiping ? mSwipeDistance : 0f;
        }
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
        UpdateLastSwipePosition();
        mSwipeDistance = 0f;
        mSwipeTime = 0f;
        AddSwipeTime();
        AddSwipeDistance();
    }

    //TODO: Find a better ways
    //This code is optimized for the jump in balloune but isn't generic
    //Author: VBernier
    private void UpdateSwipe()
    {
        AddSwipeTime();
        AddSwipeDistance();

        if (mSwipeTime.IsBetweenExclusively(0f, mSwipeMaxTime) && mSwipeDistance > Screen.height / 20 && !IsSwiping) //To change
        {  
            var angle = Mathf.Atan2(mTouch.position.y - mStartSwipePosition.y, mTouch.position.x - mStartSwipePosition.x) * 180 / Mathf.PI;

            if (angle.IsBetweenInclusively(MIN_ANGLE, MAX_ANGLE))
            {
                SwipeDirection = ESwipeDirection.UP;
                IsSwiping = true;
            }
            else if (angle.IsBetweenInclusively(-MAX_ANGLE, -MIN_ANGLE))
            {
                SwipeDirection = ESwipeDirection.DOWN;
            }
            else if (angle.IsBetweenInclusively(-MIN_ANGLE, MIN_ANGLE))
            {
                SwipeDirection = ESwipeDirection.RIGHT;
            }
            else
            {
                SwipeDirection = ESwipeDirection.LEFT;
            }
        }
        else if (mSwipeTime >= mSwipeMaxTime)
        {
            mCanBeSwipe = false;
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
        mSwipeTime += mTouch.deltaTime;
    }

    private void AddSwipeDistance()
    {
        mSwipeDistance += mTouch.deltaPosition.magnitude;
        UpdateLastSwipePosition();
    }

    private void UpdateLastSwipePosition()
    {
        mLastSwipePosition = mTouch.position;
    }

    public void OnStationary()
    {
        mStationaryTime += mTouch.deltaTime;
    }
}
