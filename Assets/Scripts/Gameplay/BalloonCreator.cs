using UnityEngine;
using System.Collections;

public class BalloonCreator 
{
	private static GameObject mBalloonPrefab;
	//private static GameObject mRopePrefab;
	private static GameObject mTack;

	public BalloonCreator(GameObject pBalloonPrefab, GameObject pTack)
	{
		mBalloonPrefab = pBalloonPrefab;
		//mRopePrefab = pRopePrefab;
		mTack = pTack;
	}
	
	public GameObject CreateBalloon(Vector2 pPosition)
	{
		GameObject balloon = PrefabFactory.Instantiate (mBalloonPrefab, pPosition);
		return balloon;
	}
}
