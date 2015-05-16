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
		GameObject ropeGameObject = PrefabFactory.Instantiate (mRopePrefab, new Vector2 (pBalloon.transform.position.x, 3));
		Rope rope = ropeGameObject.GetComponent<Rope> ();
		rope.createRope (GetMaxBalloonDistance (pBalloon));
		return rope;
	}
	
	private float GetMaxBalloonDistance(GameObject pBalloonObject)
	{
		Balloon balloon = pBalloonObject.GetComponent<Balloon> ();

		return balloon.m_MaxBalloonDistance;
		/*DistanceJoint2D balloonJoint= pBalloonObject.GetComponent<DistanceJoint2D> ();
		balloonJoint.connectedBody = mTack.GetComponent<Rigidbody2D>();
		return balloonJoint.distance;*/
	}
	
	public void AttachRope(GameObject pBalloonObject, Rope pRope)
	{
		DistanceJoint2D balloonJoint= pBalloonObject.GetComponent<DistanceJoint2D> ();
		balloonJoint.distance = GetMaxBalloonDistance (pBalloonObject);
		balloonJoint.connectedBody = mTack.GetComponent<Rigidbody2D>();
		AttachRopeToCharacter(pRope);
		AttachRopeToBalloon(pBalloonObject, pRope);
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
