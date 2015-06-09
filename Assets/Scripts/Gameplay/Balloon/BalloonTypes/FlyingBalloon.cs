/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class FlyingBalloon : Balloon {
	
	public const float FLYING_ROPE_DISTANCE = 7f;
	
	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(Color.white);
		AddBehavior<CharacterPullBehavior>();
		AddBehavior<FlyingBehavior>();
		AddBehavior<TriggerableBehavior>();
		m_MaxRopeDistance = FLYING_ROPE_DISTANCE;
	}
}
