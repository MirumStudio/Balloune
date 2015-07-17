/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class PressurePlate : Trigger {
	
	private SliderJoint2D mSliderJoint;
	private float mHalfLimits = 0f;
	
	protected void Start () {
		base.Start ();
		mSliderJoint = GetComponent<SliderJoint2D> ();
		mHalfLimits = ((Mathf.Abs (mSliderJoint.limits.max) - Mathf.Abs (mSliderJoint.limits.min)) / 2);
	}

	private void FixedUpdate()
	{
		CheckIfPressed ();
	}

	private void CheckIfPressed()
	{
		if (mSliderJoint.jointTranslation >= mSliderJoint.limits.max) {
			base.SetIsTriggered(true);
		} else {
			base.SetIsTriggered(false);
		}
	}
}
