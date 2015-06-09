/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class CharacterPull
{
	private float mPullStrength = 0f;
	private int mPullDirection = 0;

	public CharacterPull (){}

	public void SetPullStrength(double pPullAngle)
	{
		mPullStrength = CalculatePullStrength (pPullAngle);
		SetPullDirection (mPullStrength);
	}

	private float CalculatePullStrength(double pPullAngle)
	{
		float pPullAngleFloat = Mathf.Abs ((float) pPullAngle);
		float pullStrength = (90 - pPullAngleFloat) / 90;

		return pullStrength;
	}

	public float GetPullStrength()
	{
		return mPullStrength;
	}

	private void SetPullDirection(double pPullDirection)
	{
		if (pPullDirection > 0) {
			mPullDirection = 1;
		} else if (pPullDirection < 0) {
			mPullDirection = -1;
		} else {
			mPullDirection = 0;
		}
	}

	public void SetPullDirection(int pPullDirection)
	{
		mPullDirection = pPullDirection;
	}

	public int GetPullDirection()
	{
		return mPullDirection;
	}

	public bool IsPulling()
	{
        return (mPullStrength != 0);
	}

	public void StopPulling()
	{
		mPullStrength = 0f;
		mPullDirection = 0;
	}
}

