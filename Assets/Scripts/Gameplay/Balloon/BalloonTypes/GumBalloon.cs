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

	private bool mIsAttachedToKid = true;

	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(new Color(1f, 0.53f, 1f));
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxRopeDistance = GUM_ROPE_DISTANCE;
	}

	protected override void Update()
	{
		base.Update();
		Orient ();
	}

	protected override void Deflate() {
		base.mIsInflating = false;
		base.mIsDeflating = true;
		if (transform.localScale.magnitude >= (base.mBaseScale.magnitude / INFLATE_FACTOR)) {
			transform.localScale = transform.localScale * 0.99f;
		} else {
			base.mIsDeflating = false;
		}
	}

	private void Orient()
	{
		if (mIsAttachedToKid) {
			//TODO change this if the kid is facing left
			Quaternion rotation = Quaternion.Euler (0, 0, -110);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 10);
		}
	}

	public void SetIsAttachedToKid(bool pIsAttachedToKid)
	{
		mIsAttachedToKid = pIsAttachedToKid;
	}
}
