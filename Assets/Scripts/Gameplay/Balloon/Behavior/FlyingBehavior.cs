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
	private DetachBehavior mDetachBehavior;

	protected override void Start () {
		base.Start ();
		EventService.Register<AttachBalloonDelegate>(EGameEvent.ATTACH_BALLOON, OnAttachBalloon);
		mDetachBehavior = GetComponent<DetachBehavior> ();
	}
	
	void Update () {
		
	}
	
	private void OnAttachBalloon(Balloon pBalloon, GameObject pTack)
	{
		if (((Balloon)pBalloon).GameObject == mBalloon.GameObject) {
			MoveableObject flyingObject = mBalloon.BalloonHolder.Owner.GetComponent<MoveableObject>();
			if(flyingObject != null)
			{
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
	}
	
}