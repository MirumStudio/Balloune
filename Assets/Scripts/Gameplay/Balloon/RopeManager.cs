using UnityEngine;
using System.Collections;

public class RopeManager 
{
	private GameObject mRopePrefab;
	private GameObject mTack;
	
	public RopeManager(GameObject pRopePrefab, GameObject pTack)
	{
		mRopePrefab = pRopePrefab;
		mTack = pTack;
	}
	
	public Rope CreateRopeForBalloon(GameObject pBalloon)
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
	
	public void AttachRope(GameObject pBalloon, Rope pRope)
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
		pRope.transform.parent = pBalloon.transform;
	}
}
