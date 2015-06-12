/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using System;
using UnityEngine;

public class FlyingBehavior : BalloonBehavior
{
	protected override void Start () {
		base.Start ();
		AllowFlight ();
		//TODO this will be used if/when balloons are not automatically attached to the girl upon creation
		//EventListener.Register (EGameEvent.ATTACH_BALLOON, OnAttachBalloon);
	}
	
	void Update () {
		
	}

	private void OnAttachBalloon(Enum pEvent, object pBalloon, object pTack)
	{
		if (((Balloon)pBalloon).GameObject == mBalloon.GameObject) {
			AllowFlight ();
		}
	}

	private void AllowFlight()
	{
		DistanceJoint2D existingDistanceJoint = mBalloon.Physics.DistanceJoint2D;
		GameObject flyingCharacterObject = mBalloon.BalloonHolder.Owner;
		//TODO dans les niveaux qui peuvent contenir une source de gaz "flying", il faudrait que les personnages aient déjà ce newDistanceJoint
		DistanceJoint2D newDistanceJoint = flyingCharacterObject.AddComponent<DistanceJoint2D> ();
		newDistanceJoint = CopyDistanceJoint2D (existingDistanceJoint, newDistanceJoint);
		newDistanceJoint.connectedBody = mBalloon.Physics.GetRigidBody();
	}

	private DistanceJoint2D CopyDistanceJoint2D (DistanceJoint2D pJointToCopy, DistanceJoint2D pNewDistanceJoint)
	{
		pNewDistanceJoint.connectedAnchor = pJointToCopy.anchor;
		pNewDistanceJoint.distance = pJointToCopy.distance;
		pNewDistanceJoint.maxDistanceOnly = pJointToCopy.maxDistanceOnly;
		return pNewDistanceJoint;
	}

	public override void OnPop()
	{
	}
}
