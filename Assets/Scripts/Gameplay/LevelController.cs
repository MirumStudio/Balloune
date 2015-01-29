using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {
    private int mBalloonGivenCount = 0;

	// Use this for initialization
	protected void Start () {
        EventListener.Register(EGameEvent.CHILD_COLLISION, OnChildCollision);
	}
	
	// Update is called once per frame
	void Update () {
	    if(mBalloonGivenCount >= LevelInfo.ChildCount)
        {
            OnFinish();
        }
	}

    private void OnFinish()
    {
        EventService.DipatchEvent(EGameEvent.LEVEL_FINISHED, null);
    }

    private void OnChildCollision(Enum lol, System.Object arg)
    {
        EventService.DipatchEvent(EGameEvent.BALLOON_GIVEN, arg);
        mBalloonGivenCount++;
    }
}
