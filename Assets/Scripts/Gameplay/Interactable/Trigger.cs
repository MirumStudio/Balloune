/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class Trigger : MonoBehaviour {
	
	private const int AREA_OF_EFFECT = 3;

	[SerializeField]
	private bool m_IsActivatedByStunBalloon = false;

	protected bool mIsTriggered = false;
	
	protected void Start () {
		if (m_IsActivatedByStunBalloon) {
			EventService.Register<Vector2Delegate>(EGameEvent.STUN_BALLOON_POP, OnStunBalloonPop);
		}
	}
	
	protected virtual void Activate()
	{
		if (mIsTriggered == true) {
			EventService.DispatchEvent (EGameEvent.TRIGGER_OBJECT, this);
		} else {
			EventService.DispatchEvent(EGameEvent.STOP_TRIGGER_OBJECT, this);
		}
	}
	
	public bool IsTriggered()
	{
		return mIsTriggered;
	}

	public void SetIsTriggered(bool pIsTriggered)
	{
		if (mIsTriggered != pIsTriggered) {
			mIsTriggered = pIsTriggered;
			Activate ();
		}
	}

	protected void OnStunBalloonPop(Vector2 pPos)
	{
		if(IsInAOE(pPos))
		{
			EventService.DispatchEvent (EGameEvent.TRIGGER_OBJECT, this);
		}
	}
		
	protected bool IsInAOE(Vector2 pPos)
	{
		float distance = Vector2.Distance(this.transform.position, pPos);
		return distance <= AREA_OF_EFFECT;
	}
}
