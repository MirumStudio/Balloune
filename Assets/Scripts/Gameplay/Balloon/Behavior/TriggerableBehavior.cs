/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;

public class TriggerableBehavior : BalloonBehavior
{
	protected override void Start () {
		base.Start ();
        EventService.Register<BalloonDelegate>(EGameEvent.TRIGGER_BALLOON, OnTriggerBalloon);
	}
	
	void Update () {
		
	}

	public void OnTriggerBalloon(Balloon pBalloon)
	{
		if (pBalloon.GameObject == mBalloon.GameObject) { //&& !mBalloon.Physics.IsAttached) {
			mBalloon.Physics.PopBalloon();
		}
	}

	public override void OnPop()
	{
		if (!mBalloon.Physics.IsAttached) {
			DestroyObject (mBalloon.GameObject);
		}
	}
}
