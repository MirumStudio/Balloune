using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TouchControl
{
    public Touch mTouch;

    public TouchControl(Touch pTouch)
    {
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

    public bool IsSwiping
    {
        private set;
        get;
    }

    public float SwipeDistance
    {
        get
        {
            return IsSwiping ? Vector2.Distance(mStartSwipePosition, mTouch.position) : 0f ;
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

        mSwipeTime = 0f;
        AddSwipeTime();
    }

    private void UpdateSwipe()
    {
        AddSwipeTime();

        if (mSwipeTime > 0.3f && SwipeDistance > 0f)
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
        //DANGEROUS
        mSwipeTime += mTouch.deltaTime;
    }
}
