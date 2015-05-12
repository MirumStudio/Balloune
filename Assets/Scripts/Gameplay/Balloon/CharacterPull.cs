using UnityEngine;
using System.Collections.Generic;

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
		float pullStrength = (float)(90 - pPullAngle) / 90;
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
		bool isPulling = false;
		if (mPullStrength != 0) {
			isPulling = true;
		}
		return isPulling;
	}

	public void StopPulling()
	{
		mPullStrength = 0f;
		mPullDirection = 0;
	}
}

