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

public class TimedTriggerableGasSource : GasSource {
	
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
		if (mIsTriggered) {
			IncrementEmissionTimer();
		}
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
	
	private void IncrementEmissionTimer()
	{
			mActivatedTime += Time.deltaTime;
			if(mActivatedTime >= EMISSION_TIME)
			{
				StopEmission();
			}
	}
	
	private void CreateGasPoints()
	{
		mGasPoints.Add (new TimedGasPoint (GetColliderMaxBound ()));
		mGasPoints.Add (new TimedGasPoint (transform.position));
		mGasPoints.Add (new TimedGasPoint (GetColliderMinBound ()));
		for(int i = 0; i < mGasPoints.Count; i++)
		{
			TimedGasPoint tempTimedGasPoint = ((TimedGasPoint)mGasPoints[i]);
			if(tempTimedGasPoint.GetMaxActivationTime() == 0f)
			{
				tempTimedGasPoint.SetMaxActivationTime(EMISSION_TIME);
			}
		}
	}
	
	protected override void UpdateGasPoints()
	{
		if (mGasPoints.Count == 0) {
			CreateGasPoints ();
		} else {
			for(int i = 0; i < mGasPoints.Count; i++)
			{
				TimedGasPoint tempTimedGasPoint = ((TimedGasPoint)mGasPoints[i]);
				tempTimedGasPoint.CheckTime();
			}
			ActivateGasPoints();
		}
	}
	
	private void ActivateGasPoints()
	{
		if (ShouldActivateGasPoint()) {
			float currentAngle = 0f;
			if(!IsFirstGasPoint())
			{
				currentAngle = mGasPoints[mPreviouslyActivatedGasPoint].TotalAngle;
			}
			mPreviouslyActivatedGasPoint++;
			((TimedGasPoint)mGasPoints [mPreviouslyActivatedGasPoint]).SetIsActive (true);
			mGasPoints[mPreviouslyActivatedGasPoint].TotalAngle = currentAngle;
			
		} else if (IsLastGasPoint()){
			mPreviouslyActivatedGasPoint = -1;
			StopEmission ();
		}
	}
	
	private bool ShouldActivateGasPoint()
	{
		return ((IsFirstGasPoint () && mIsTriggered) || (!IsFirstGasPoint() && !IsPreviousGasPointActive() && !IsLastGasPoint ()));
	}
	
	private bool IsFirstGasPoint()
	{
		return (mPreviouslyActivatedGasPoint == -1);
	}
	
	private bool IsPreviousGasPointActive()
	{
		return (mPreviouslyActivatedGasPoint != -1 && ((TimedGasPoint)mGasPoints [mPreviouslyActivatedGasPoint]).IsActive () == true);
	}
	
	private bool IsLastGasPoint()
	{
		return (mPreviouslyActivatedGasPoint == mGasPoints.Count - 1);
	}
}
