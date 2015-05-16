using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class DetachBehavior : BalloonBehavior
{
	private BalloonPhysics mPhysics;
	
	protected override void Start () {
		base.Start ();
		mPhysics = GetComponent<BalloonPhysics>();
		}
	
	void Update () {
		
	}

	void FixedUpdate() {
		DetachBalloon ();
	}

	private void DetachBalloon()
	{
		mPhysics.DetachBalloon ();
	}
}
