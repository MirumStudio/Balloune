/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using UnityEngine;

public class CharacterPullBehavior : BalloonBehavior
{

    private CharacterPull mCharacterPull;
    private BalloonPhysics mPhysics;
    private BalloonHolder mBalloonHolder;

	protected override void Start () {
		base.Start ();
		mPhysics = mBalloon.Physics;
        mBalloonHolder = GetComponent<BalloonHolder>();
        EventService.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

    public void OnStopPulling()
    {
        mCharacterPull = null;
    }

    public override void OnMove(float pDistance)
    {
        if (mCharacterPull == null)
        {
            mCharacterPull = new CharacterPull();
            EventService.DispatchEvent(EGameEvent.BEGIN_PULLING, mCharacterPull, mBalloon);
        }
        DragCharacter();
    }

    private void DragCharacter()
    {
        float balloonAngle = (float)mPhysics.GetBalloonAngle();
        Vector2 balloonPos = mPhysics.transform.position;
        mCharacterPull.SetBalloonInfo(balloonPos, balloonAngle);
    }

    public bool IsPullingCharacter()
    {
        return (mCharacterPull != null && mCharacterPull.IsPulling());
    }
}
