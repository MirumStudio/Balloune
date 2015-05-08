using UnityEngine;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour {
	//Code taken from http://www.thegamecontriver.com/2014/08/unity3d-swipe-input-for-touch-screen.html
	private int currentTouchIndex = -1;
	private float mMinimumSwipeDistance;
	private float mLeftOfScreen;
	private float mRightOfScreen;
	private List<Vector3> mLeftTouchPositions = new List<Vector3>();
	
	private static float mSpeed = 0;
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
		if (touch.phase == TouchPhase.Ended) {
			mSpeed = 0;
			ClearTouches ();
			Debug.Log ("Ended");
		} else {
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				Touch newTouch = touch;
				AddTouchPosition(newTouch);
				if(IsTurnAround())
				{
					Vector3 newStartingTouch = mLeftTouchPositions[currentTouchIndex - 1];
					ClearTouches ();
					AddPosition(newStartingTouch);
					AddTouchPosition(newTouch);
				}
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
		float swipeDistance = mLeftTouchPositions [0].y - mLeftTouchPositions [currentTouchIndex].y;
		Debug.Log ("Start : " + mLeftTouchPositions [0].y);
		Debug.Log ("End : " + mLeftTouchPositions [currentTouchIndex].y);
		Debug.Log ("Distance : " + swipeDistance);
		mSpeed = GetFractionOfScreenHeight (swipeDistance);
		Debug.Log ("Screen Height : " + Screen.height);
		Debug.Log ("Fraction of Screen Height : " + mSpeed);
	}
	
	private void AddTouchPosition(Touch touch)
	{
		mLeftTouchPositions.Add(touch.position);
		currentTouchIndex++;
	}

	private void AddPosition(Vector3 position)
	{
		mLeftTouchPositions.Add (position);
		currentTouchIndex++;
	}
	
	private void ClearTouches()
	{
		mLeftTouchPositions.Clear ();
		currentTouchIndex = -1;
	}
	
	public static float GetSpeed()
	{
		return mSpeed;
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
		return isSwipeBigEnough;
	}

	private bool IsTurnAround()
	{
		//To call after adding new touch
		bool isTurnAround = false;
		if (mLeftTouchPositions.Count > 1) {
			bool originalDirectionIsUp = true;
			if ((mLeftTouchPositions [currentTouchIndex - 1].y - mLeftTouchPositions [0].y) < 0) {
				originalDirectionIsUp = false;
			}
			bool currentDirectionIsUp = true;
			if ((mLeftTouchPositions[currentTouchIndex].y - mLeftTouchPositions[currentTouchIndex - 1].y) < 0) {
				currentDirectionIsUp = false;
			}
			if(originalDirectionIsUp != currentDirectionIsUp)
			{
				isTurnAround = true;
			}
		}

		return isTurnAround;
	}
	
	private float GetFractionOfScreenHeight(float distance)
	{
		float fractionOfScreenHeight = distance / Screen.height;
		return fractionOfScreenHeight;
	}
}
