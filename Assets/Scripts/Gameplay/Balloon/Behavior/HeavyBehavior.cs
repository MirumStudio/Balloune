/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

public class HeavyBehavior : BalloonBehavior
{
	protected override void Start () {
		base.Start ();
		mBalloon.GravityScale = 1f;
		mBalloon.Physics.GetRigidBody ().gravityScale = mBalloon.GravityScale;
	}
	
	void Update () {
		
	}
	
	void FixedUpdate() {
		
	}
}
