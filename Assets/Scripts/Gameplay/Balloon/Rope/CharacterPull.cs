/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class CharacterPull
{
    public float Strength
    {
        get;
        private set;
    }

    public EDirection Direction
    {
        get;
        private set;
    }

    public CharacterPull()
    {
        Strength = 0f;
        Direction = EDirection.NONE;
    }

    public void SetBalloonInfo(Vector2 pPosition, float pAngle)
    {
        float distance = CalculateDistance(pPosition);
        CalculatePullDirection(pAngle);
        CalculatePullStrength(distance);
    }

    private float CalculateDistance(Vector2 pPosition)
    {
        float distance = Vector2.Distance(pPosition, TouchService.CurrentTouchPosition);
        
        float distanceMax = 5;
        
        distance = Mathf.Min(distance, distanceMax);
        
        distance = distance * 0.7f / distanceMax;
        distance += 0.35f;

        return distance;
    }

	private void CalculatePullStrength(float pDistance)
	{
        Strength = Direction.Sign() * pDistance;
    }

    private void CalculatePullDirection(double pPullAngle)
	{
        float pPullAngleFloat = Mathf.Abs((float)pPullAngle);
        float pullStrength = (90 - pPullAngleFloat) / 90;

        if (pullStrength > 0.1) {
			Direction = EDirection.RIGHT;
        } else if (pullStrength < -0.1) {
            Direction = EDirection.LEFT;
		} else {
            Direction = EDirection.NONE;
		}
	}

	public bool IsPulling()
	{
        return (Strength != 0);
	}

	public void StopPulling()
	{
		Strength = 0f;
		Direction = EDirection.NONE;
	}
}

