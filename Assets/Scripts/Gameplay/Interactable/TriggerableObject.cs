/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class TriggerableObject : MonoBehaviour {
	
	private const int AREA_OF_EFFECT = 3;

	[SerializeField]
	private Trigger m_Trigger;

	protected bool mIsTriggered = false;
	
	
	protected virtual void Start () {
		EventService.Register<TriggerObjectDelegate> (EGameEvent.TRIGGER_OBJECT, OnTrigger);
	}
	
	protected virtual void OnTrigger(Trigger pTrigger)
	{
		if(m_Trigger != null && (m_Trigger.gameObject == pTrigger.gameObject))
		{
			Trigger();
		}
	}

	protected virtual void Trigger ()
	{}

	protected bool IsNearGearBox(Vector2 pPos)
	{
		float distance = Vector2.Distance(m_Trigger.transform.position, pPos);
		return distance <= AREA_OF_EFFECT;
	}
}
