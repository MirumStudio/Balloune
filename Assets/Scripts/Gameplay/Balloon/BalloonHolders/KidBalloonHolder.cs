/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;

public class KidBalloonHolder : BalloonHolder
{
	public const string KID_BALLOON_HOLDER_NAME = "KidBalloonHolder";
	protected override void Start()
	{
		base.Start ();
		m_MaxBalloonCount = 1;
	}

	protected override void AttachBalloon(Balloon pBalloon)
	{
		EventService.DispatchEvent (EGameEvent.BALLOON_GIVEN, pBalloon);
		base.AttachBalloon (pBalloon);
	}

	public override void DetachBalloon(Balloon pBalloonToDetach)
	{
		EventService.DispatchEvent (EGameEvent.BALLOON_TAKEN, pBalloonToDetach);
		base.DetachBalloon (pBalloonToDetach);
	}
}

