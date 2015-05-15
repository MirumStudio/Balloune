using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class LifeBehavior : BalloonBehavior
{
    private BalloonPhysics mPhysics;

	void Start () {
        mPhysics = GetComponent<BalloonPhysics>();

	}

	void Update () {
	    
	}

    public override void OnPop()
    {
       // CheckIfGameOver();
        base.OnPop();
    }

    private void CheckIfGameOver()
    {
       /* if (mBalloonHolder.GetLifeBalloonCount() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }*/
    }
}
