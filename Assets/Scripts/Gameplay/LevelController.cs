using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {
    private int mBalloonGivenCount = 0;

    bool mIsFinished = false;

	protected void Start () {
        EventListener.Register(EGameEvent.CHILD_COLLISION, OnChildCollision);
	}
	
	void Update () {
        if (mBalloonGivenCount >= LevelInfo.ChildCount && !mIsFinished)
        {
            OnFinish();
        }
	}

    private void OnFinish()
    {
        mIsFinished = true;
        EventService.DispatchEvent(EGameEvent.LEVEL_FINISHED, null);
    }

    private void OnChildCollision(Enum pEnum, System.Object arg)
    {
        EventService.DispatchEvent(EGameEvent.BALLOON_GIVEN, arg);
        mBalloonGivenCount++;
    }
}
