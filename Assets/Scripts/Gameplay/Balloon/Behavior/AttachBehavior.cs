using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class AttachBehavior : BalloonBehavior
{	
	protected override void Start () {
		base.Start ();
		EventListener.Register(EGameEvent.DROP_BALLOON, OnDropBalloon);
	}
	
	void Update () {
		
	}
	
	void FixedUpdate() {

	}
	
	private void OnDropBalloon(Enum pEvent, object pBalloon)
	{
		if (((Balloon) pBalloon).GameObject == mBalloon.GameObject) {
			Vector2 position = transform.position;
			EventService.DispatchEvent(EGameEvent.ATTEMPT_ATTACH_BALLOON, pBalloon, position);
		}
	}
}
