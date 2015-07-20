/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class TimedGasPoint : GasPoint {

	private float mMaxActivationTime = 0f;

	private float mActivatedTime = 0f;
	private bool mIsActive = false;
	
	public TimedGasPoint(Vector2 pPosition) : base(pPosition)
	{
	}

	public float GetMaxActivationTime()
	{
		return mMaxActivationTime;
	}

	public void SetMaxActivationTime(float pMaxActivationTime)
	{
		mMaxActivationTime = pMaxActivationTime;
	}

	public void CheckTime()
	{
		if (mIsActive) {
			mActivatedTime += Time.deltaTime;
			if (mActivatedTime >= mMaxActivationTime) {
				mIsActive = false;
				mActivatedTime = 0f;
			}
		}
	}

	public bool IsActive() {
		return mIsActive;
	}

	public void SetIsActive(bool pIsActive)
	{
		mIsActive = pIsActive;
	}
}
