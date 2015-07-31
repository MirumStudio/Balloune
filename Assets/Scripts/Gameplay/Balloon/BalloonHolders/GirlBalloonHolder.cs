/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using Radix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GirlBalloonHolder : BalloonHolder
{
	public const string GIRL_BALLOON_HOLDER_NAME = "GirlBalloonHolder";
	public const int MAX_LIFE_BALLOON = 3;
	public const int MAX_SPECIAL_BALLOON = 3;

	private int mNumberOfLifeBalloons = 0;
	private int mNumberOfSpecialBalloons = 0;

	protected override void Start()
	{
		base.Start ();
        EventService.Register<BalloonTypeDelegate>(EGameEvent.INFLATE_BALLOON, OnInflateBalloon);
		
		for (int i = 0; i < LevelInfo.StartLifeBalloonCount; i++)
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
		if (pType.Equals (EBalloonType.LIFE) && !HoldsMaximumLifeBalloons()) {
			mNumberOfLifeBalloons++;
			base.CreateBalloon (pType);
		} else if (!pType.Equals (EBalloonType.LIFE) && !pType.Equals (EBalloonType.GUM) && !HoldsMaximumSpecialBalloons()) {
			mNumberOfSpecialBalloons++;
			base.CreateBalloon(pType);
		}
	}

	public override void DestroyBalloon(Balloon pBalloon)
	{
		base.DestroyBalloon (pBalloon);
		if (GetLifeBalloonCount() <= 0)
		{
			EventService.DispatchEvent(EGameEvent.GAME_OVER);
		}
	}

	private bool HoldsMaximumLifeBalloons()
	{
		bool holdsMaximumLifeBalloons = false;
		if (mNumberOfLifeBalloons >= MAX_LIFE_BALLOON) {
			holdsMaximumLifeBalloons = true;
		}
		return holdsMaximumLifeBalloons;
	}

	private bool HoldsMaximumSpecialBalloons()
	{
		bool holdsMaximumSpecialBalloons = false;
		if (mNumberOfSpecialBalloons >= MAX_SPECIAL_BALLOON) {
			holdsMaximumSpecialBalloons = true;
		}
		return holdsMaximumSpecialBalloons;
	}

	public override void DetachBalloon(Balloon pBalloonToDetach)
	{
		if (pBalloonToDetach.Type.Equals (EBalloonType.LIFE)) {
			mNumberOfLifeBalloons--;
		} else {
			mNumberOfSpecialBalloons--;
		}
		base.DetachBalloon (pBalloonToDetach);
	}

	public override void PopRandomBalloon()
	{
		List<Balloon> lifeBalloons = GetLifeBalloons ();
		if (lifeBalloons.Count > 0) {
			int balloonIndexToPop = UnityEngine.Random.Range(0, mNumberOfLifeBalloons - 1);
			lifeBalloons[balloonIndexToPop].Physics.PopBalloon ();
		}
	}

	private List<Balloon> GetLifeBalloons()
	{
		return mBalloons.FindAll((balloon) => 
		{
			return balloon is LifeBalloon;
		});
	}
}

