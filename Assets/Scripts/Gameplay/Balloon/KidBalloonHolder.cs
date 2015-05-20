using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class KidBalloonHolder : BalloonHolder
{
	public const string KID_BALLOON_HOLDER_NAME = "KidBalloonHolder";
	protected override void Start()
	{
		base.Start ();
		m_MaxBalloonCount = 1;
	}
}

