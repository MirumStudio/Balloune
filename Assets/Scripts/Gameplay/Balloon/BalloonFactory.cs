using UnityEngine;
using System.Collections;
using Radix.ErrorMangement;

public class BalloonFactory 
{
	private static GameObject mBalloonPrefab;
	//private static GameObject mRopePrefab;
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

    public static GameObject CreateBalloon(Vector2 pPosition)
    {
        Assert.CheckNull(mBalloonPrefab);
        Assert.CheckNull(mTack);

        return PrefabFactory.Instantiate(mBalloonPrefab, pPosition);
    }
}
