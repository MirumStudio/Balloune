using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

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
