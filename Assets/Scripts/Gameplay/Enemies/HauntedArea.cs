/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections.Generic;

public class HauntedArea : MonoBehaviour {

	[SerializeField]
	private Light m_AreaLight;
	private HauntedLight mHauntedLight;

	private float mScreamInterval = 2f;
	private float mScreamTimer = 0f;
	private bool mIsScreaming = false;

	private List<BalloonHolder> mBalloonHolders = new List<BalloonHolder>();

	void Start () {
		mHauntedLight = m_AreaLight.GetComponent<HauntedLight> ();
	}

	private void Update()
	{
		Scream ();
	}

	public void OnTriggerEnter2D(Collider2D pCollider)
	{
		Balloon balloon = pCollider.gameObject.GetComponent<Balloon>();
		if (balloon != null && balloon.BalloonHolder != null && !mBalloonHolders.Contains(balloon.BalloonHolder))
		{
			mBalloonHolders.Add(balloon.BalloonHolder);
			StartScreamTimer();
		}
	}

	public void OnTriggerExit2D(Collider2D pCollider)
	{
		Balloon balloon = pCollider.gameObject.GetComponent<Balloon>();
		if (balloon != null && balloon.BalloonHolder != null && !mBalloonHolders.Contains(balloon.BalloonHolder))
		{
			//Because of this, balloons still inside might be invulnerable to a scream.
			//This is as designed, as they are now very close to the exit
			mBalloonHolders.Remove(balloon.BalloonHolder);
			StopScreamTimer();
		}
	}

	private void StartScreamTimer()
	{
		mIsScreaming = true;
	}

	private void Scream()
	{
		if (mIsScreaming) {
			mScreamTimer += Time.deltaTime;
			if (mScreamTimer >= mScreamInterval) {
				mScreamTimer = 0f;
				for(int i = 0; i < mBalloonHolders.Count; i++)
				{
					mBalloonHolders[i].PopRandomBalloon();
				}
			}
		}
	}

	private void StopScreamTimer()
	{
		//TODO make sure there is no balloon left inside the area
		mIsScreaming = false;
	}

	private void LightUp()
	{
		m_AreaLight.range = m_AreaLight.range * 2;
		mHauntedLight.StopOscillation ();
	}

}
