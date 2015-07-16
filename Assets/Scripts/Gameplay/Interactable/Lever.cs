using UnityEngine;
using System.Collections;
using Radix.Event;

public class Lever : MoveableObject {

	private HingeJoint2D mHingeJoint;

	private int mHalfAngleLimits = 0;
	private bool mIsTriggered = false;

	protected override void Start () {
		base.Start ();
		mHingeJoint = GetComponent<HingeJoint2D> ();
		mHalfAngleLimits = (int)((mHingeJoint.limits.max - mHingeJoint.limits.min) / 2);
		mHalfAngleLimits = Mathf.Abs (mHalfAngleLimits);
	}

	private void FixedUpdate()
	{
		Trigger ();
	}

	private void Trigger()
	{
		if (Mathf.Abs(mHingeJoint.jointAngle) > mHalfAngleLimits) {
			mIsTriggered = true;
		}
	}

	public bool IsTriggered()
	{
		return mIsTriggered;
	}
}
