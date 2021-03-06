/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TriggerableGasSource : GasSource {

	private const float EMISSION_TIME = 2f;

	[SerializeField]
	private Trigger m_Trigger;

	private float mActivatedTime = 0f;

	private bool mIsTriggered = false;

	private ParticleSystem mParticleSystem;

	private int mPreviouslyActivatedGasPoint = -1;
	
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
		base.Update ();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	protected override void VerifyCircle (Vector2 pPos)
	{
		for (int i = 0; i < mGasPoints.Count; i++) {
			if((mGasPoints[i]).IsActive())
			{
				base.VerifyCircleForGasPoint(i, pPos);
			}
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

	private void CreateGasPoints()
	{
		mGasPoints.Add (new GasPoint (GetColliderMaxBound ()));
		mGasPoints.Add (new GasPoint (transform.position));
		mGasPoints.Add (new GasPoint (GetColliderMinBound ()));
	}
	
	protected override void UpdateGasPoints()
	{
		if (mGasPoints.Count == 0) {
			CreateGasPoints ();
		} else {
			base.UpdateGasPoints();
		}
	}
}
