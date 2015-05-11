using UnityEngine;
using System.Collections.Generic;

public class CharacterPull
{
	private float mPullStrength = 0f;
	private int mPullDirection = 0;

	public CharacterPull (){}

	public void SetPullStrength(float pPullStrength)
	{
		mPullStrength = pPullStrength;
	}

	public float GetPullStrength()
	{
		return mPullStrength;
	}

	public void SetPullDirection(float pPullDirection)
	{
		mPullDirection = -1;
		if (pPullDirection > 0) {
			mPullDirection = 1;
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
		if (mPullStrength > 0) {
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

