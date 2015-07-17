/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class Door : TriggerableObject {

	private SliderJoint2D mSliderJoint;
	
	protected override void Start () {
		base.Start ();
		mSliderJoint = GetComponent<SliderJoint2D> ();
	}
	

	protected override void Trigger()
	{
		RaiseDoor ();
	}
	
	protected void RaiseDoor()
	{
		mSliderJoint.useMotor = true;
	}
}
