using UnityEngine;
using System.Collections.Generic;

public class SwipeControl : MonoBehaviour {
	//Code taken from http://www.thegamecontriver.com/2014/08/unity3d-swipe-input-for-touch-screen.html
	private Vector3 mFirstTouchPosition;
	private Vector3 mLastTouchPosition;
	private float mMinimumSwipeDistance;
	private float mLeftOfScreen;
	private float mRightOfScreen;
	private List<Vector3> mTouchPositions = new List<Vector3>();

	private static float mDirection = 0;
	private static bool mIsJumpCommand = false;

	// Use this for initialization
	void Start () {
		mMinimumSwipeDistance = Screen.height * 5 / 100; //dragDistance is 20% height of the screen
		mLeftOfScreen = Screen.width * 20 / 100;
		mRightOfScreen = Screen.width * 80 / 100;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches)
		{ 	
			if (touch.phase == TouchPhase.Moved) //add the touches to list as the swipe is being made
			{
				mTouchPositions.Add(touch.position);
				mLastTouchPosition =  mTouchPositions[mTouchPositions.Count-1]; //last touch position 
				if(mTouchPositions.Count == 1)
				{
					mFirstTouchPosition =  mTouchPositions[0]; //get first touch position from the list of touches
				}
			}
			
			//Check if drag distance is greater than 20% of the screen height
			if (IsSwipeBiggerThanMinimumDistance() && mTouchPositions.Count > 1)
			{//It's a drag
				if(mFirstTouchPosition.x < mLeftOfScreen)
				{
					if (mLastTouchPosition.y > mFirstTouchPosition.y)  //If the movement was up
					{   //Up swipe
						//Debug.Log("Up Swipe"); 
						mDirection = -1;
					}
					else
					{   //Down swipe
						//Debug.Log("Down Swipe");
						mDirection = 1;
					}
				}
			} 
			
			if (mFirstTouchPosition.x > mRightOfScreen)
			{
				mIsJumpCommand = true;
			}
			
			if (touch.phase == TouchPhase.Ended) {
				mTouchPositions.Clear();
				mIsJumpCommand = false;
				mDirection = 0;
			}
		}
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
