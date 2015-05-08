using UnityEngine;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour {
	//Code taken from http://www.thegamecontriver.com/2014/08/unity3d-swipe-input-for-touch-screen.html
	private int currentTouchIndex = -1;
	private float mMinimumSwipeDistance;
	private float mLeftOfScreen;
	private float mRightOfScreen;
	private List<Vector3> mLeftTouchPositions = new List<Vector3>();

	private static float mDirection = 0;
	private static bool mIsJumpCommand = false;

	void Start () {
		mMinimumSwipeDistance = Screen.height * 5 / 100; //dragDistance is 20% height of the screen
		mLeftOfScreen = Screen.width * 20 / 100;
		mRightOfScreen = Screen.width * 80 / 100;
	}

	void Update () {
		EvaluateTouchCommand();
	}

	private void EvaluateTouchCommand ()
	{
		foreach (Touch touch in Input.touches)
		{ 	
			if (touch.position.x > mRightOfScreen) {
				EvaluateRightSide (touch);
			} else if ( touch.position.x < mLeftOfScreen){
				EvaluateLeftSide(touch);
			}
		}
	}

	private void EvaluateLeftSide(Touch touch)
	{
		if (touch.phase == TouchPhase.Stationary) {
			ClearTouches ();
		} else if (touch.phase == TouchPhase.Ended) {
			mDirection = 0;
			ClearTouches ();
		} else {
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				AddTouchPosition(touch);
			}
			if (IsSwipeBiggerThanMinimumDistance ()){
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
		if (mLeftTouchPositions[currentTouchIndex].y > mLeftTouchPositions[0].y) 
		{   
			IsSwipingUp = true;
		}
		return IsSwipingUp;
	}

	private void AddTouchPosition(Touch touch)
	{
		mLeftTouchPositions.Add(touch.position);
		currentTouchIndex++;
	}

	private void ClearTouches()
	{
		mLeftTouchPositions.Clear ();
		currentTouchIndex = -1;
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
		float swipeDistance = Mathf.Abs (mLeftTouchPositions[currentTouchIndex].y - mLeftTouchPositions[0].y);
		//minSwipeDistance = 26
		isSwipeBigEnough = swipeDistance > mMinimumSwipeDistance;
		//return true;
		return isSwipeBigEnough;
	}
}
