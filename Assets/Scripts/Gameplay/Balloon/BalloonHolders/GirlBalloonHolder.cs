/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using Radix.Utilities;
using System;

public class GirlBalloonHolder : BalloonHolder
{
	public const string GIRL_BALLOON_HOLDER_NAME = "GirlBalloonHolder";
	public const int MAX_LIFE_BALLOON = 3;

	private int mNumberOfLifeBalloons = 0;
	protected override void Start()
	{
		base.Start ();
        EventService.Register<BalloonTypeDelegate>(EGameEvent.INFLATE_BALLOON, OnInflateBalloon);
		
		for (int i = 0; i < MAX_LIFE_BALLOON; i++)
		{
			CreateBalloon(EBalloonType.LIFE);
		}
	}

    private void OnInflateBalloon(EBalloonType pType)
	{	
		CreateBalloon(pType);
	}

	public override void CreateBalloon(EBalloonType pType)
	{
		if (pType.Equals (EBalloonType.LIFE) && CanCreateAdditionalLifeBalloon (pType)) {
			mNumberOfLifeBalloons++;
			base.CreateBalloon (pType);
		} else if (!pType.Equals (EBalloonType.LIFE)) {
			base.CreateBalloon(pType);
		}
	}

	private bool CanCreateAdditionalLifeBalloon(EBalloonType pType)
	{
		bool shouldCreateAdditionalLifeBalloon = true;
		if (HoldsMaximumLifeBalloons()) {
			shouldCreateAdditionalLifeBalloon = false;
		}
		return shouldCreateAdditionalLifeBalloon;
	}

	private bool HoldsMaximumLifeBalloons()
	{
		bool holdsMaximumLifeBalloons = false;
		if (mNumberOfLifeBalloons >= MAX_LIFE_BALLOON) {
			holdsMaximumLifeBalloons = true;
		}
		return holdsMaximumLifeBalloons;
	}

	public override void DetachBalloon(Balloon pBalloonToDetach)
	{
		if (pBalloonToDetach.Type.Equals (EBalloonType.LIFE)) {
			mNumberOfLifeBalloons--;
		}
		base.DetachBalloon (pBalloonToDetach);
	}
}

