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
	private const float TIME_BETWEEN_PARTICLES = 0.05f;
	[SerializeField]
	private GameObject m_GasTrail;
	private ParticleSystem mParticleGenerator;

	private float mParticleGenerationTime = 0f;
	private bool isGeneratingParticles = false;

	protected override void Start () {
		base.Start ();
		EventService.Register<BalloonDelegate>(EGameEvent.TRIGGER_BALLOON, OnTriggerBalloon);

		//Change GasTrail color according to the balloon's color

		mParticleGenerator = mBalloon.gameObject.AddComponent<ParticleSystem> ();
		mParticleGenerator.enableEmission = false;
	}
	
	void Update () {
		if (isGeneratingParticles) {
			GenerateTrail();
		}
	}
	
	public void OnTriggerBalloon(Balloon pBalloon)
	{
		if (pBalloon != null && pBalloon.GameObject == mBalloon.GameObject)
		{
			isGeneratingParticles = true;
			//Start emission of "spray" particles
			//Prevent this balloon from being attached to anything
			//Make this balloon disappear after a certain period of time
		}
	}

	private void GenerateTrail()
	{
		mParticleGenerationTime += Time.deltaTime;
		if (mParticleGenerationTime >= TIME_BETWEEN_PARTICLES) {
			//PrefabFactory.Instantiate (m_GasTrail, transform.position);
			mParticleGenerationTime = 0f;
		}
	}
}
