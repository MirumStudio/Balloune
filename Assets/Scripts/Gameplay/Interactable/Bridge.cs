/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class Bridge : TriggerableObject {

    private HingeJoint2D mHinge;

	void Start () {
		base.Start ();
        mHinge = GetComponent<HingeJoint2D>();
	}

	protected override void Trigger()
	{
		DropBridge ();
	}

    private void DropBridge()
    {
        JointAngleLimits2D newLimits = new JointAngleLimits2D();
        newLimits.min = 0;
        newLimits.max = mHinge.limits.max;
        mHinge.limits = newLimits;
    }
}
