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

	[SerializeField]
	private bool m_IsTimed;
	private float mActivatedTime = 0f;

	private bool mIsTriggered = false;

	private ParticleSystem mParticleSystem;

	private int mPreviouslyActivatedGasPoint = 0;
	
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
			ActivateGasPoints();
			IncrementEmissionTimer();
		}
	}

	protected override void FixedUpdate()
	{
		if (mIsTriggered) {
			base.FixedUpdate();
		}
	}

	protected override void VerifyCircle (Vector2 pPos)
	{
		if (mIsTriggered == true) {
			for (int i = 0; i < mGasPoints.Count; i++) {
				if(((TimedGasPoint)mGasPoints[i]).IsActive())
				{
					Debug.Log ("Verifying circle for timed gas point");
					base.VerifyCircleForGasPoint(i, pPos);
				}
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

	protected override void UpdateGasPoints()
	{
		mGasPoints.Add (new TimedGasPoint (GetColliderMaxBound()));
		mGasPoints.Add (new TimedGasPoint (transform.position));
		mGasPoints.Add (new TimedGasPoint (GetColliderMinBound()));
		float maxActivationTime = EMISSION_TIME / mGasPoints.Count;
		for(int i = 0; i < mGasPoints.Count; i++)
		{
			if(((TimedGasPoint)mGasPoints[i]).GetMaxActivationTime() == 0f)
			{
				((TimedGasPoint)mGasPoints[i]).SetMaxActivationTime(maxActivationTime);
			}
			((TimedGasPoint)mGasPoints[i]).CheckTime();
		}
	}

	private void ActivateGasPoints()
	{
		if (((TimedGasPoint)mGasPoints[mPreviouslyActivatedGasPoint]).IsActive() == false && mPreviouslyActivatedGasPoint < mGasPoints.Count) {
			mPreviouslyActivatedGasPoint++;
			((TimedGasPoint)mGasPoints[mPreviouslyActivatedGasPoint]).SetIsActive(true);
		}
	}
}
