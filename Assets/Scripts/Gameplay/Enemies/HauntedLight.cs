/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class HauntedLight : MonoBehaviour {
	private const float LIGHT_VARIATION = 20f;
	private const float LIGHT_INCREMENT = 0.5f;

	private Light mLight;

	private float mMinRange = 0f;
	private float mBaseRange = 0f;
	private float mMaxRange = 0f;

	private bool mIsDimming = true;
	private bool mIsOscillating = true;

	void Start () {
		mLight = GetComponent<Light> ();
		mBaseRange = mLight.range;
		mMinRange = mBaseRange - LIGHT_VARIATION;
		mMaxRange = mBaseRange + LIGHT_VARIATION;
	}

	private void Update()
	{
		Oscillate ();
	}

	private void Oscillate()
	{
		if (mIsOscillating) {
			if (mIsDimming) {
				Dim ();
			} else {
				Shine ();
			}
		}
	}

	public void StopOscillation()
	{
		mIsOscillating = false;
	}

	private void Dim()
	{
		if (mLight.range > mMinRange) {
			mLight.range = mLight.range - LIGHT_INCREMENT;
		} else {
			mIsDimming = false;
		}
	}

	private void Shine()
	{
		if (mLight.range < mMaxRange) {
			mLight.range = mLight.range + LIGHT_INCREMENT;
		} else {
			mIsDimming = true;
		}
	}
}
