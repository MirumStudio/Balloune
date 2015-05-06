using UnityEngine;
using System.Collections;

public class BalloonCreator : MonoBehaviour 
{
	private GameObject mBalloonPrefab;
	private GameObject mRopePrefab;
	private GameObject mTack;

	public BalloonCreator(GameObject pBalloonPrefab, GameObject pRopePrefab, GameObject pTack)
	{
		mBalloonPrefab = pBalloonPrefab;
		mRopePrefab = pRopePrefab;
		mTack = pTack;
	}
	
	public GameObject CreateBalloon(Vector2 pPosition)
	{
		GameObject balloon = PrefabFactory.Instantiate (mBalloonPrefab, pPosition);
		Rope newRope = CreateRopeForBalloon (balloon);
		AttachRope (balloon, newRope);
		return balloon;
	}

	private Rope CreateRopeForBalloon(GameObject pBalloon)
	{
		float maxBalloonDistance = SetMaxBalloonDistance (pBalloon);
		GameObject ropeGameObject = PrefabFactory.Instantiate (mRopePrefab, new Vector2 (pBalloon.transform.position.x, 3));
		Rope rope = ropeGameObject.GetComponent<Rope> ();
		rope.createRope (maxBalloonDistance);
		return rope;
	}

	private float SetMaxBalloonDistance(GameObject pBalloon)
	{
		DistanceJoint2D balloonJoint= pBalloon.GetComponent<DistanceJoint2D> ();
		balloonJoint.connectedBody = mTack.GetComponent<Rigidbody2D>();
		return balloonJoint.distance;
	}

	private void AttachRope(GameObject pBalloon, Rope pRope)
	{
		AttachRopeToCharacter(pRope);
		AttachRopeToBalloon(pBalloon, pRope);
	}
	
	private void AttachRopeToCharacter(Rope pRope)
	{
		pRope.GetStartOfRope().GetComponent<HingeJoint2D>().connectedBody = mTack.GetComponent<Rigidbody2D> ();
	}
	
	private void AttachRopeToBalloon(GameObject pBalloon, Rope pRope)
	{
		HingeJoint2D balloonHinge = pBalloon.GetComponent<HingeJoint2D> ();
		balloonHinge.connectedBody = pRope.GetEndOfRope().GetComponent<Rigidbody2D>();
		balloonHinge.connectedAnchor = new Vector2 (0, pRope.GetLengthOfEachSegment());
	}
}
