using UnityEngine;
using System.Collections.Generic;
using Radix.Event;
using System;

[RequireComponent (typeof(Camera))]
public class BalloonControl : MonoBehaviour {
	private const string BALLOON_IDENTIFIER = "Balloon";

	private GameObject mTouchedBalloonObject = null;
	private Balloon mTouchedBalloon = null;
    private BalloonPhysics mTouchedBalloonPhysics = null;
	private static Vector2 mWorldPosition;

	private float mMinimumSwipeDistance;
	private int mCurrentTouchIndex = -1;

	private float mSwipeSpeedThreshold = 1.2f;
	private static bool mIsJumpCommand = false;	

	private float mDoubleTapThreshold = 0.2f;
	private float mDoubleTapTime = 0f;

	void Start () {
		mMinimumSwipeDistance = Screen.height * 8 / 100;
        EventListener.Register(ETouchEvent.TAP, OnTap);
        EventListener.Register(ETouchEvent.DOUBLE_TAP, OnDoubleTap);
        EventListener.Register(ETouchEvent.END, OnEndTouch);
        EventListener.Register(ETouchEvent.SWIPE_BEGIN, OnSwipeBegin);
        EventListener.Register(ETouchEvent.SWIPE_END, OnSwipeEnd);
	}
	
	void Update () {
	//	EvaluateTouchCommand();
	}
	

    private void OnTap(Enum pEvent, object pArg)
    {
        var touchedBallon = GetTouchedBalloon((Vector2)pArg);

        if(touchedBallon != null)
        {
            mTouchedBalloonObject = touchedBallon;
            PickupBalloon();
        }
    }

    private void OnDoubleTap(Enum pEvent, object pArg)
    {

    }

    private void OnEndTouch(Enum pEvent, object pArg)
    {
        if (mTouchedBalloonObject != null)
        {
            DropBalloon();
        }
    }

    private void OnSwipeBegin(Enum pEvent, object pArg)
    {
        var lol = (Vector2)pArg;
        if(mTouchedBalloonObject != null)
        {
            mIsJumpCommand = true;
        }
    }

    private void OnSwipeEnd(Enum pEvent, object pArg)
    {
        if(mTouchedBalloonObject != null)
        {
            mIsJumpCommand = false;
        }
    }

	private void EvaluateTouchCommand ()
	{
		int tapCount = Input.touchCount;
		for (int i = 0; i < tapCount; i++)
		{ 	
			if(Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
			{			
				AddTouch(Input.GetTouch(i));
				mWorldPosition = GetWorldPosition(Input.GetTouch(i));
				if(mTouchedBalloonObject == null)
				{
					mTouchedBalloonObject = GetTouchedBalloon(mWorldPosition);
				}
				if(mTouchedBalloonObject != null)
				{
					PickupBalloon();
					mIsJumpCommand = IsJumpCommand(Input.GetTouch (i));
					IsDoubleTap(Input.GetTouch(i));
				}
			}
			else{
				DropBalloon();
			}
		}
	}

	private Vector3 GetWorldPosition(Touch pTouch)
	{
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pTouch.position);
		return worldPosition;
	}

	private GameObject GetTouchedBalloon(Vector3 pWorldPosition)
	{
		Vector2 worldPosition2D = new Vector2(pWorldPosition.x, pWorldPosition.y);
		Collider2D[] touchedColliders = Physics2D.OverlapCircleAll (worldPosition2D,  1f);
		for(int i = 0; i < touchedColliders.Length; i++)
		{
			if(touchedColliders[i].name.Contains (BALLOON_IDENTIFIER))
			{
				mTouchedBalloonObject = touchedColliders[i].gameObject;
				break;
			}
		}

		return mTouchedBalloonObject;
	}

	private bool IsJumpCommand(Touch pTouch)
	{
		bool isJumpCommand = false;
		if (mCurrentTouchIndex > 0) {
			Vector2 speedVector = pTouch.deltaTime * pTouch.deltaPosition;
			float swipeSpeed = speedVector.magnitude;
			if(swipeSpeed > mSwipeSpeedThreshold && pTouch.deltaPosition.y > mMinimumSwipeDistance && Mathf.Abs(pTouch.deltaPosition.x) < 100)
			{
				isJumpCommand = true;
			}
			else{
				isJumpCommand = false;
			}
		}
		return isJumpCommand;
	}

	private bool IsDoubleTap(Touch pTouch)
	{
		bool isDoubleTap = false;
		if (pTouch.phase == TouchPhase.Began && mDoubleTapTime > 0f && mDoubleTapTime <= mDoubleTapThreshold) {
			//this is a double tap
			isDoubleTap = true;
			EventService.DispatchEvent(EGameEvent.TRIGGER_BALLOON, mTouchedBalloon);
			mDoubleTapTime = 0f;
		} else if (pTouch.phase == TouchPhase.Began) {
			//First touch ever
			mDoubleTapTime = 0f;
		} else if(pTouch.phase == TouchPhase.Began) {
			//Any touch that is not a double tap
			mDoubleTapTime = 0f;
		} else if (pTouch.phase != TouchPhase.Began) {
			//Any touch that stayed on the screen
			mDoubleTapTime += pTouch.deltaTime;
		}
		return isDoubleTap;
	}

	private void PickupBalloon()
	{
        if (mTouchedBalloon == null)
        {
            mTouchedBalloon = mTouchedBalloonObject.GetComponent<Balloon>();
            mTouchedBalloonPhysics = mTouchedBalloon.Physics;
            EventService.DispatchEvent(EGameEvent.PICKUP_BALLOON, mTouchedBalloon);
        }
	}

	private void DropBalloon()
	{
		if (mTouchedBalloonObject != null) {
			EventService.DispatchEvent(EGameEvent.DROP_BALLOON, mTouchedBalloon);
			mTouchedBalloonObject = null;
			mTouchedBalloonPhysics = null;
			mTouchedBalloon = null;
		}
		mWorldPosition = Vector2.zero;
		mCurrentTouchIndex = -1;
	}

	private void AddTouch(Touch touch)
	{
		mCurrentTouchIndex++;
	}

	public static bool IsJumpCommand()
	{
		return mIsJumpCommand;
	}
}
