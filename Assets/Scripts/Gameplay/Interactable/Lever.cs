using UnityEngine;
using System.Collections;
using Radix.Event;

[RequireComponent (typeof (Trigger))]
public class Lever : MoveableObject {

	private HingeJoint2D mHingeJoint;

	private int mHalfAngleLimits = 0;

	private Trigger mTrigger;

	protected override void Start () {
		base.Start ();
		mHingeJoint = GetComponent<HingeJoint2D> ();
		mHalfAngleLimits = (int)((mHingeJoint.limits.max - mHingeJoint.limits.min) / 2);
		mHalfAngleLimits = Mathf.Abs (mHalfAngleLimits);
		mTrigger = GetComponent<Trigger> ();
	}

	private void FixedUpdate()
	{
		Trigger ();
	}

	private void Trigger()
	{
		if (Mathf.Abs (mHingeJoint.jointAngle) > mHalfAngleLimits) {
			mTrigger.SetIsTriggered(true);
		} else {
			mTrigger.SetIsTriggered(false);
		}
	}
}
