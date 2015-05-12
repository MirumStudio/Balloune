using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(Camera))]
public class TouchControl : MonoBehaviour {
	private const string BALLOON_IDENTIFIER = "Balloune";

	private GameObject mTouchedBalloon = null;
	private BalloonBehavior mTouchedBalloonBehavior = null;
	private static Vector2 mWorldPosition;

	private float mMinimumSwipeDistance;
	private List<Vector3> mTouchPositions = new List<Vector3>();
	private int mCurrentTouchIndex = -1;

	private float mSwipeThreshold = 1.8f;
	private static bool mIsJumpCommand = false;	

	void Start () {
		mMinimumSwipeDistance = Screen.height * 8 / 100;
	}
	
	void Update () {
		EvaluateTouchCommand();
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
				if(mTouchedBalloon == null)
				{
					mTouchedBalloon = GetTouchedBalloon(mWorldPosition);
				}
				if(mTouchedBalloon != null)
				{
					PickupBalloon();
					mIsJumpCommand = IsJumpCommand(Input.GetTouch (i));
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

	private bool IsJumpCommand(Touch pTouch)
	{
		bool isJumpCommand = false;
		if (mCurrentTouchIndex > 0) {
			Vector2 speedVector = pTouch.deltaTime * pTouch.deltaPosition;
			float swipeSpeed = speedVector.magnitude;
			if(swipeSpeed > mSwipeThreshold && pTouch.deltaPosition.y > 0 && Mathf.Abs(pTouch.deltaPosition.x) < 25)
			{
				isJumpCommand = true;
			}
			else{
				isJumpCommand = false;
			}
		}
		return isJumpCommand;
	}

	private void PickupBalloon()
	{
		mTouchedBalloonBehavior = mTouchedBalloon.GetComponent<BalloonBehavior>();
		mTouchedBalloonBehavior.setIsTouched(true);
		mTouchedBalloonBehavior.IgnoreOtherBalloonCollision (true);
		mTouchedBalloonBehavior.GetRigidBody().gravityScale = 0;
		mTouchedBalloonBehavior.GetRigidBody().drag = 0;
	}

	private void DropBalloon()
	{
		if (mTouchedBalloon != null) {
			mTouchedBalloonBehavior.setIsTouched(false);
			mTouchedBalloonBehavior.IgnoreOtherBalloonCollision (false);
			mTouchedBalloonBehavior.GetRigidBody().drag = 1;
			mTouchedBalloonBehavior.GetRigidBody().gravityScale = -1;
			mTouchedBalloonBehavior.GetPull().StopPulling();
			mTouchedBalloon = null;
			mTouchedBalloonBehavior = null;
		}
		mWorldPosition = Vector2.zero;
		mTouchPositions.Clear ();
		mCurrentTouchIndex = -1;
	}

	private void AddTouch(Touch touch)
	{
		mTouchPositions.Add (touch.position);
		mCurrentTouchIndex++;
	}

	public static bool IsJumpCommand()
	{
		return mIsJumpCommand;
	}
}
