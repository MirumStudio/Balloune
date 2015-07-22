/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;
using UnityEngine;
using Radix.Event;

public class FlyingBehavior : BalloonBehavior
{
	private int MAX_FLYING_TIME = 10;

	private DetachBehavior mDetachBehavior;
	private AttachBehavior mAttachBehavior;

	private float mFlyingTime = 0f;
	private bool mIsAttachedToMoveableObject = false;

	protected override void Start () {
		base.Start ();
		EventService.Register<AttachBalloonDelegate>(EGameEvent.ATTACH_BALLOON, OnAttachBalloon);
		mDetachBehavior = GetComponent<DetachBehavior> ();
		mAttachBehavior = GetComponent<AttachBehavior> ();
	}
	
	void Update () {
		DeflateBalloon ();
	}
	
	private void OnAttachBalloon(Balloon pBalloon, GameObject pTack)
	{
		if (((Balloon)pBalloon).GameObject == mBalloon.GameObject) {
			MoveableObject flyingObject = mBalloon.BalloonHolder.Owner.GetComponent<MoveableObject>();
			if(flyingObject != null)
			{
				mIsAttachedToMoveableObject = true;
				AllowFlight (flyingObject);
				DisallowDetach();
			}
		}
	}
	
	private void AllowFlight(MoveableObject pFlyingObject)
	{
		DistanceJoint2D existingDistanceJoint = mBalloon.Physics.DistanceJoint2D;
		existingDistanceJoint.enabled = false;
		DistanceJoint2D objectDistanceJoint = pFlyingObject.GetDistanceJoint ();
		objectDistanceJoint.connectedBody = mBalloon.Physics.GetRigidBody();
		objectDistanceJoint.enabled = true;
	}

	private void DisallowDetach()
	{
		mDetachBehavior.enabled = false;
		mAttachBehavior.enabled = false;
	}

	private void DeflateBalloon()
	{
		if (mBalloon.Physics.IsTouched && mIsAttachedToMoveableObject == true) {
			mFlyingTime += Time.deltaTime;
			mBalloon.Shrink();
			if(mFlyingTime >= MAX_FLYING_TIME)
			{
				mBalloon.Physics.PopBalloon();
			}

		}
	}
	
}