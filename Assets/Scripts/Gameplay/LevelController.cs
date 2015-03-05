﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {
    private int mBalloonGivenCount = 0;

    bool testFinish = false;

	protected void Start () {
        EventListener.Register(EGameEvent.CHILD_COLLISION, OnChildCollision);
	}
	
	void Update () {
        if (mBalloonGivenCount >= LevelInfo.ChildCount && !testFinish)
        {
            OnFinish();
        }
	}

    private void OnFinish()
    {
        testFinish = true;
        EventService.DipatchEvent(EGameEvent.LEVEL_FINISHED, null);
    }

    private void OnChildCollision(Enum lol, System.Object arg)
    {
        EventService.DipatchEvent(EGameEvent.BALLOON_GIVEN, arg);
        mBalloonGivenCount++;
    }
}
