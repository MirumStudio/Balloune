using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(Camera))]
public class TouchControl : MonoBehaviour {
	private const string BALLOON_IDENTIFIER = "Balloune";

	private GameObject mTouchedBalloon = null;
	private BalloonBehavior mTouchedBalloonBehavior = null;
	private static Vector2 mWorldPosition;

	private float mMinimumSwipeDistance;
	private float mLeftOfScreen;
	private float mRightOfScreen;
	private List<Vector3> mLeftTouchPositions = new List<Vector3>();
	
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
			if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
			{			
				mWorldPosition = GetWorldPosition(touch);
				if(mTouchedBalloon == null)
				{
					mTouchedBalloon = GetTouchedBalloon(mWorldPosition);
					if(mTouchedBalloon != null)
					{
						mTouchedBalloonBehavior = mTouchedBalloon.GetComponent<BalloonBehavior>();
						mTouchedBalloonBehavior.setIsTouched(true);
					}
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
			if(touchedColliders[i].name.Contains ("Balloune"))
			{
				mTouchedBalloon = touchedColliders[i].gameObject;
				break;
			}
		}

		return mTouchedBalloon;
	}

	public static Vector2 GetTouchPosition()
	{
		return mWorldPosition;
	}

	private void DropBalloon()
	{
		mTouchedBalloonBehavior.setIsTouched(false);
		mTouchedBalloon = null;
		mTouchedBalloonBehavior = null;
		mWorldPosition = Vector2.zero;
	}
}
