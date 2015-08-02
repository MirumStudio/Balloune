/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class GumBalloon : Balloon {
	
	public const float GUM_ROPE_DISTANCE = 0.1f;
	
	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(new Color(1f, 0.53f, 1f));
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxRopeDistance = GUM_ROPE_DISTANCE;
	}

	protected override void Deflate() {
		base.mIsInflating = false;
		base.mIsDeflating = true;
		if (transform.localScale.magnitude > (base.mBaseScale.magnitude / INFLATE_FACTOR)) {
			transform.localScale = transform.localScale * 0.95f;
		} else {
			base.mIsDeflating = false;
		}
	}

	protected override void UpdateCenterOfMass()
	{
		mCenterOfMass.x = SpriteRenderer.bounds.size.x / 2;
		mCenterOfMass.x = mCenterOfMass.x * -1;
		Physics.GetRigidBody ().centerOfMass = mCenterOfMass;
	}

}
