using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class GirlBalloonHolder : BalloonHolder
{
	public const string GIRL_BALLOON_HOLDER_NAME = "GirlBalloonHolder";
	public const int NUMBER_LIFE_BALLOON = 3;
	protected override void Start()
	{
		base.Start ();
		EventListener.Register(EGameEvent.INFLATE_BALLOON, OnInflateBalloon);
		
		for (int i = 0; i < NUMBER_LIFE_BALLOON; i++)
		{
			CreateBalloon(EBalloonType.LIFE);
		}
	}

	private void OnInflateBalloon(Enum pEvent, object pArg)
	{
		var type = EnumUtility.ObjectToEnum<EBalloonType>(pArg);
		
		CreateBalloon(type);
	}
}

