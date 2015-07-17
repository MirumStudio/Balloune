/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class StunBalloon : Balloon {
	
	public const float STUN_ROPE_DISTANCE = 1.2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.yellow);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxRopeDistance = STUN_ROPE_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
