/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using UnityEngine;

public class TrailBehavior : BalloonBehavior
{
	private const float TIME_BETWEEN_PARTICLES = 0.02f;
	private const float DEFLATION_TIME = 10f;

	[SerializeField]
	private GameObject m_GasTrail = null;
	private GameObject mCustomizedGasTrail;
	private ParticleSystem mParticleGenerator;

	private AttachBehavior mAttachBehavior;

	private float mParticleGenerationTime = 0f;
	private float mDeflationTime = 0f;
	private bool mIsDeflating = false;

	protected override void Start () {
		base.Start ();
		EventService.Register<BalloonDelegate>(EGameEvent.TRIGGER_BALLOON, OnTriggerBalloon);
		mAttachBehavior = GetComponent<AttachBehavior> ();

		mCustomizedGasTrail = m_GasTrail;
		mCustomizedGasTrail.GetComponent<ParticleSystem> ().startColor = mBalloon.SpriteRenderer.color;
		Debug.Log ("mCustomizedGasTrail has been instanced");
		mParticleGenerator = mBalloon.gameObject.AddComponent<ParticleSystem> ();
		mParticleGenerator.enableEmission = false;
	}
	
	void Update () {
		if (mIsDeflating) {
			GenerateTrail();
			CheckIfDeflated();
		}
	}
	
	public void OnTriggerBalloon(Balloon pBalloon)
	{
		if (pBalloon != null && pBalloon.GameObject == mBalloon.GameObject)
		{
			mIsDeflating = true;
			PreventAttaching();
			//Start emission of "spray" particles
		}
	}

	private void PreventAttaching()
	{
		if(mAttachBehavior != null)
		{
			mAttachBehavior.enabled = false;
		}
	}
	
	private void GenerateTrail()
	{
		mParticleGenerationTime += Time.deltaTime;
		if (mParticleGenerationTime >= TIME_BETWEEN_PARTICLES) {
			PrefabFactory.Instantiate (mCustomizedGasTrail, transform.position);
			mParticleGenerationTime = 0f;
		}
	}

	private void CheckIfDeflated()
	{
		mDeflationTime += Time.deltaTime;
		if (mDeflationTime >= DEFLATION_TIME) {
			mBalloon.Physics.PopBalloon();
		}
	}
}
