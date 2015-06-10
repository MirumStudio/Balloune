/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using UnityEngine;

public class AttachBehavior : BalloonBehavior
{	
	protected override void Start () {
		base.Start ();
		EventService.Register<BalloonDelegate>(EGameEvent.DROP_BALLOON, OnDropBalloon);
	}
	
	void Update () {
		
	}
	
	void FixedUpdate() {

	}
	
	private void OnDropBalloon(Balloon pBalloon)
	{
		if (pBalloon.GameObject == mBalloon.GameObject) {
			Vector2 position = transform.position;
			EventService.DispatchEvent(EGameEvent.ATTEMPT_ATTACH_BALLOON, pBalloon, position);
		}
	}
}
