using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class CharacterPullBehavior : BalloonBehavior
{

    private CharacterPull mCharacterPull;
    private BalloonPhysics mPhysics;
    private BalloonHolder mBalloonHolder;

	protected override void Start () {
		base.Start ();
		mPhysics = mBalloon.Physics;
        mBalloonHolder = GetComponent<BalloonHolder>();
        EventListener.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

    public void OnStopPulling(Enum pEvent, object pArg)
    {
        mCharacterPull = null;
    }

    public override void OnMove(float pDistance)
    {
        if (mCharacterPull == null)
        {
            mCharacterPull = new CharacterPull();
            EventService.DispatchEvent(EGameEvent.BEGIN_PULLING, mCharacterPull);
        }
        DragCharacter();
    }

    private void DragCharacter()
    {
        double balloonAngle = mPhysics.GetBalloonAngle();
        mCharacterPull.SetPullStrength(balloonAngle);
        float mDirection = mCharacterPull.GetPullDirection();
    }

    public bool IsPullingCharacter()
    {
        return (mCharacterPull != null && mCharacterPull.IsPulling());
    }
}
