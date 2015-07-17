/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {
	
	public const float TOXIC_ROPE_DISTANCE = 1.2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.green);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		GetComponent<TrailBehavior> ().enabled = true;
		m_MaxRopeDistance = TOXIC_ROPE_DISTANCE;
	}
}
