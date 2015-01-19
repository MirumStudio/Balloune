using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {
    public GameObject but;

    private int mBalloonGivenCount = 0;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        but.SetActive(false);
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
        but.SetActive(true);
    }

    private void OnChildCollision(Enum lol, System.Object arg)
    {
        EventService.DipatchEvent(EGameEvent.BALLOON_GIVEN, arg);
        mBalloonGivenCount++;
    }
}
