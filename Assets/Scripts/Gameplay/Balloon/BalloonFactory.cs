using UnityEngine;
using System.Collections;
using Radix.ErrorMangement;

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
