using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {
    private int mBalloonGivenCount = 0;

    bool mIsFinished = false;

	protected void Start () {
		EventListener.Register(EGameEvent.BALLOON_GIVEN, OnBalloonGiven);
		EventListener.Register(EGameEvent.BALLOON_TAKEN, OnBalloonTaken);
		EventListener.Register(EGameTrigger.LEVEL_END_REACHED, OnLevelEndReached);
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

    private void OnBalloonGiven(Enum pEnum, object arg)
    {
        mBalloonGivenCount++;
    }

	private void OnBalloonTaken(Enum pEnum, object arg)
	{
		mBalloonGivenCount--;
	}

	private void OnLevelEndReached(Enum pEnum, object arg)
	{
		OnFinish ();
	}
}
