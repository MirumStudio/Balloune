using UnityEngine;
using System.Collections.Generic;
using Radix.Event;

[RequireComponent (typeof(Camera))]
public class TouchControl : MonoBehaviour {
	private const string BALLOON_IDENTIFIER = "Balloon";

	private GameObject mTouchedBalloon = null;
    private BalloonPhysics mTouchedBalloonPhysics = null;
	private static Vector2 mWorldPosition;

	private float mMinimumSwipeDistance;
	private List<Vector3> mTouchPositions = new List<Vector3>();
	private int mCurrentTouchIndex = -1;

	private float mSwipeSpeedThreshold = 1.2f;
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
			if(touchedColliders[i].name.Contains (BALLOON_IDENTIFIER))
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

	private void PickupBalloon()
	{
        mTouchedBalloonPhysics = mTouchedBalloon.GetComponent<BalloonPhysics>();
        mTouchedBalloonPhysics.setIsTouched(true);
        mTouchedBalloonPhysics.IgnoreOtherBalloonCollision(true);
        mTouchedBalloonPhysics.GetRigidBody().gravityScale = 0;
        mTouchedBalloonPhysics.GetRigidBody().drag = 0;
	}

	private void DropBalloon()
	{
		if (mTouchedBalloon != null) {
            mTouchedBalloonPhysics.setIsTouched(false);
            mTouchedBalloonPhysics.IgnoreOtherBalloonCollision(false);
            mTouchedBalloonPhysics.GetRigidBody().drag = 1;
            mTouchedBalloonPhysics.GetRigidBody().gravityScale = -1;

            EventService.DipatchEvent(EGameEvent.END_PULLING, null);

			mTouchedBalloon = null;
            mTouchedBalloonPhysics = null;
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
