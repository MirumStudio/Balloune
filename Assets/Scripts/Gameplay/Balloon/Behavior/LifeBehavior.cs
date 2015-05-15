using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class LifeBehavior : BalloonBahavior {


    private BalloonPhysics mPhysics;
    private BalloonHolder mBalloonHolder;

	void Start () {
        mPhysics = GetComponent<BalloonPhysics>();
        mBalloonHolder = GetComponent<BalloonHolder>();
	}

	void Update () {
	
	}

    private void CheckIfGameOver()
    {
        if (mBalloonHolder.CountBalloons() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }
    }
}
