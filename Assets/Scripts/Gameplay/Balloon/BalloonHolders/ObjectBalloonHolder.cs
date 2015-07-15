/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using Radix.Event;

public class ObjectBalloonHolder : BalloonHolder
{
	public const string OBJECT_BALLOON_HOLDER_NAME = "ObjectBalloonHolder";
	private bool mIsFlying = false;
	protected override void Start()
	{
		base.Start ();
		m_MaxBalloonCount = 1;
	}
	
	protected override void AttachBalloon(Balloon pBalloon)
	{
		base.AttachBalloon (pBalloon);
		if (pBalloon.Type == EBalloonType.FLYING) {
			mIsFlying = true;
		}
	}
	
	public override void DetachBalloon(Balloon pBalloonToDetach)
	{
		base.DetachBalloon (pBalloonToDetach);
		if (pBalloonToDetach.Type == EBalloonType.FLYING) {
			mIsFlying = false;
		}
	}
}

