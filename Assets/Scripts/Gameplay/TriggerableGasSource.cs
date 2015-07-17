/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;
using System.Collections.Generic;

public class TriggerableGasSource : GasSource {

	private const float EMISSION_TIME = 2f;

	[SerializeField]
	private Trigger m_Trigger;

	[SerializeField]
	private bool m_IsTimed;
	private float mActivatedTime = 0f;

	private bool mIsTriggered = false;

	private ParticleSystem mParticleSystem;
	
	void Start()
	{
		base.Start ();
		EventService.Register<TriggerObjectDelegate> (EGameEvent.TRIGGER_OBJECT, OnTrigger);
		EventService.Register<TriggerObjectDelegate> (EGameEvent.STOP_TRIGGER_OBJECT, OnStopTrigger);
		mParticleSystem = gameObject.GetComponentInChildren<ParticleSystem> ();
		mParticleSystem.enableEmission = false;
		mEdgeCollider.enabled = false;
	}

	protected override void Update()
	{
		if (mIsTriggered) {
			base.Update ();
			IncrementEmissionTimer();
		}
	}

	protected override void FixedUpdate()
	{
		if (mIsTriggered) {
			base.FixedUpdate();
		}
	}

	protected override void VerifyCircle (Vector2 pos)
	{
		if (mIsTriggered == true) {
			base.VerifyCircle (pos);
		}
	}

	private void OnTrigger(Trigger pTrigger)
	{
		if(m_Trigger != null && (m_Trigger.gameObject == pTrigger.gameObject))
		{
			StartEmission ();
		}
	}

	private void OnStopTrigger(Trigger pTrigger)
	{
		if(m_Trigger != null && (m_Trigger.gameObject == pTrigger.gameObject))
		{
			StopEmission ();
		}
	}

	private void StartEmission()
	{
		mIsTriggered = true;
		mEdgeCollider.enabled = true;
		mParticleSystem.enableEmission = true;
	}

	private void StopEmission()
	{
		mIsTriggered = false;
		mEdgeCollider.enabled = false;
		mParticleSystem.enableEmission = false;
		mActivatedTime = 0f;
	}

	private void IncrementEmissionTimer()
	{
		if (m_IsTimed) {
			mActivatedTime += Time.deltaTime;
			if(mActivatedTime >= EMISSION_TIME)
			{
				StopEmission();
			}
		}
	}
}
