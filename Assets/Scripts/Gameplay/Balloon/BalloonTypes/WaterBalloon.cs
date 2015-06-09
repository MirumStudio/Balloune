/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class WaterBalloon : Balloon {
	
	public const float WATER_ROPE_DISTANCE = 0.5f;
	
	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(Color.blue);
		AddBehavior<DetachBehavior> ();
		AddBehavior<AttachBehavior> ();
		AddBehavior<HeavyBehavior>();
		AddBehavior<TriggerableBehavior>();
		m_MaxRopeDistance = WATER_ROPE_DISTANCE;
	}
}
