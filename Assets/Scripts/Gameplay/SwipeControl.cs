using UnityEngine;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour {
	//Code taken from http://www.thegamecontriver.com/2014/08/unity3d-swipe-input-for-touch-screen.html
	private Vector3 mFirstTouchPosition;
	private Vector3 mPreviousTouchPosition;
	private Vector3 mLastTouchPosition;
	private float mMinimumSwipeDistance;
	private float mLeftOfScreen;
	private float mRightOfScreen;
	private List<Vector3> mTouchPositions = new List<Vector3>();

	private static float mDirection = 0;
	private static bool mIsJumpCommand = false;

	void Start () {
		mMinimumSwipeDistance = Screen.height * 5 / 100; //dragDistance is 20% height of the screen
		mLeftOfScreen = Screen.width * 20 / 100;
		mRightOfScreen = Screen.width * 80 / 100;
	}

	void Update () {
		foreach (Touch touch in Input.touches)
		{ 	
			EvaluateTouchCommand(touch);
		}
	}

	private void EvaluateTouchCommand (Touch touch)
	{
		if (touch.position.x > mRightOfScreen) {
			EvaluateRightSide (touch);
		} else if ( touch.position.x < mLeftOfScreen){
			EvaluateLeftSide(touch);
		}
	}

	private void EvaluateLeftSide(Touch touch)
	{
		if (mFirstTouchPosition.x < mLeftOfScreen) {
			if (touch.phase == TouchPhase.Began) {
				AddFirstTouchPosition(touch);
			} else if (touch.phase == TouchPhase.Moved) {
				float previousDirection = mDirection;
				if(mTouchPositions.Count > 1)
				{
					AddTouchPosition (touch);
				}
				else
				{
					AddFirstTouchPosition(touch);
				}
			} else if (touch.phase == TouchPhase.Stationary) {
				ClearTouches ();
			} else if (touch.phase == TouchPhase.Ended) {
				ClearTouches ();
				mDirection = 0;
			}

			if (IsSwipeBiggerThanMinimumDistance () && mTouchPositions.Count > 1) {
				EvaluateSwipe();
			}
		}
	}

	private void EvaluateRightSide(Touch touch)
	{
		if (touch.position.x > mRightOfScreen && touch.phase != TouchPhase.Ended) {
			mIsJumpCommand = true;
		} else if (touch.phase == TouchPhase.Ended) {
			mIsJumpCommand = false;
		}
	}

	private void EvaluateSwipe()
	{
		if (IsSwipingUp ()) {
			mDirection = -1;
		} else {
			mDirection = 1;
		}
	}

	private bool IsSwipingUp()
	{
		bool IsSwipingUp = false;
		if (mLastTouchPosition.y > mFirstTouchPosition.y)  
		{   
			IsSwipingUp = true;
		}
		return IsSwipingUp;
	}

	private void AddFirstTouchPosition(Touch touch)
	{
		AddTouchPosition (touch);
		mFirstTouchPosition = mTouchPositions [0];
	}

	private void AddTouchPosition(Touch touch)
	{
		mTouchPositions.Add(touch.position);
		mPreviousTouchPosition = mLastTouchPosition;
		mLastTouchPosition =  mTouchPositions[mTouchPositions.Count-1];
	}

	private void ClearTouches()
	{
		mTouchPositions.Clear ();
	}

	public static float GetDirection()
	{
		return mDirection;
	}

	public static bool IsJumpCommand()
	{
		return mIsJumpCommand;
	}

	private bool IsSwipeBiggerThanMinimumDistance()
	{
		bool isSwipeBigEnough = false;
		isSwipeBigEnough = Mathf.Abs (mLastTouchPosition.x - mFirstTouchPosition.x) > mMinimumSwipeDistance || Mathf.Abs (mLastTouchPosition.y - mFirstTouchPosition.y) > mMinimumSwipeDistance;
		return isSwipeBigEnough;
	}
}
