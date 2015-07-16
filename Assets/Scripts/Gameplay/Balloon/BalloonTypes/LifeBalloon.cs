/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class LifeBalloon : Balloon {

	public const float LIFE_ROPE_DISTANCE = 2f;
	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
        ChangeColor(Color.red);
        AddBehavior<LifeBehavior>();
        AddBehavior<CharacterPullBehavior>();
		m_MaxRopeDistance = LIFE_ROPE_DISTANCE;
	}
}
