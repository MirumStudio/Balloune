using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class GirlBalloonHolder : BalloonHolder
{
	public const string GIRL_BALLOON_HOLDER_NAME = "GirlBalloonHolder";
	public const int MAX_LIFE_BALLOON = 3;
	private int mNumberOfLifeBalloons = 0;
	protected override void Start()
	{
		base.Start ();
		EventListener.Register(EGameEvent.INFLATE_BALLOON, OnInflateBalloon);
		
		for (int i = 0; i < MAX_LIFE_BALLOON; i++)
		{
			CreateBalloon(EBalloonType.LIFE);
		}
	}

	private void OnInflateBalloon(Enum pEvent, object pArg)
	{
		var type = EnumUtility.ObjectToEnum<EBalloonType>(pArg);
		
		CreateBalloon(type);
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

	public override void DetachBalloon(int pBalloonToDetach)
	{
		if (pBalloonToDetach.GetType().Equals (EBalloonType.LIFE)) {
			mNumberOfLifeBalloons--;
		}
		mBalloons.RemoveAt (pBalloonToDetach);
	}
}

