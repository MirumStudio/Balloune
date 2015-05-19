using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class LifeBehavior : BalloonBehavior
{
    private BalloonPhysics mPhysics;

	protected override void Start () {
		base.Start ();
		mPhysics = mBalloon.Physics;
	}

	void Update () {
	    
	}

    public override void OnPop()
	{
		base.OnPop();
		// CheckIfGameOver();
    }

	//Je crois qu'on avait dit que ce serait la scène qui ferait ce check là
    private void CheckIfGameOver()
    {
       /* if (mBalloonHolder.GetLifeBalloonCount() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }*/
    }
}
