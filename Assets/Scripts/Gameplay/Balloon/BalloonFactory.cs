/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using UnityEngine;

public class BalloonFactory 
{
	private static GameObject mBalloonPrefab;
	private static GameObject mTack;

    private BalloonFactory()
	{}
	
    public static void Init(GameObject pBalloonPrefab, GameObject pTack)
    {
        Assert.CheckNull(pBalloonPrefab);
        Assert.CheckNull(pTack);

        mBalloonPrefab = pBalloonPrefab;
        mTack = pTack;
    }

    public static Balloon CreateBalloon(EBalloonType pType, Vector2 pPosition)
    {
        Assert.CheckNull(mBalloonPrefab);
        Assert.CheckNull(mTack);

		Balloon balloon = null;
		GameObject balloonObject = null;
		//TODO: Do it in a better way
		switch(pType)
		{
			case EBalloonType.LIFE : 
			{
				balloonObject = InstantiateAtDistance(pPosition, LifeBalloon.LIFE_ROPE_DISTANCE);
				balloon  = balloonObject.AddComponent<LifeBalloon>();
				break;
			}
			case EBalloonType.TOXIC:
			{
				balloonObject = InstantiateAtDistance(pPosition, ToxicBalloon.TOXIC_ROPE_DISTANCE);
				balloon = balloonObject.AddComponent<ToxicBalloon>();
				break;
			}
			case EBalloonType.FLYING:
			{
				balloonObject = InstantiateAtDistance(pPosition, FlyingBalloon.FLYING_ROPE_DISTANCE);
				balloon = balloonObject.AddComponent<FlyingBalloon>();
				break;
			}
			case EBalloonType.STUN:
			{
				balloonObject = InstantiateAtDistance(pPosition, StunBalloon.STUN_ROPE_DISTANCE);
				balloon = balloonObject.AddComponent<StunBalloon>();
				break;
			}
			case EBalloonType.WATER:
			{
				balloonObject = InstantiateAtDistance(pPosition, WaterBalloon.WATER_ROPE_DISTANCE);
				balloon = balloonObject.AddComponent<WaterBalloon>();
				break;
			}
			case EBalloonType.GUM:
			{
				balloonObject = InstantiateAtDistance(pPosition, GumBalloon.GUM_ROPE_DISTANCE);
				balloon = balloonObject.AddComponent<GumBalloon>();
				break;
			}
		}
		
		balloon.Init(pType);
		
		return balloon;
    }

	private static GameObject InstantiateAtDistance(Vector2 pPosition, float distance)
	{
		pPosition.y = pPosition.y + distance;
		return PrefabFactory.Instantiate(mBalloonPrefab, pPosition);
	}
}
