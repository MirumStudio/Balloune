/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class PerfumeBalloon : Balloon {
	
	public const float PERFUME_ROPE_DISTANCE = 1f;
	
	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(new Color(1f, 0.53f, 1f));
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		GetComponent<TrailBehavior> ().enabled = true;
		m_MaxRopeDistance = PERFUME_ROPE_DISTANCE;
	}
}
