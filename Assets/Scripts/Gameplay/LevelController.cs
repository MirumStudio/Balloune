/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

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

    private void OnBalloonGiven(Enum pEnum, object pArg)
    {
        mBalloonGivenCount++;
    }

    private void OnBalloonTaken(Enum pEnum, object pArg)
	{
		mBalloonGivenCount--;
	}

    private void OnLevelEndReached(Enum pEnum, object pArg)
	{
		OnFinish ();
	}
}
