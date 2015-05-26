using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class RopeManager 
{
	private GameObject mRopePrefab;
	
	public RopeManager(GameObject pRopePrefab, GameObject pTack)
	{
		mRopePrefab = pRopePrefab;
		EventListener.Register(EGameEvent.ATTACH_BALLOON, OnAttachBalloon);
		//TODO
		//EventListener.Register(EGameEvent.DETACH_BALLOON, OnAttachBalloon);
	}
	
	public Rope CreateRopeForBalloon(LineRenderer pLineRenderer, GameObject pBalloon)
	{
		GameObject ropeGameObject = PrefabFactory.Instantiate (mRopePrefab, pBalloon.transform.position);
		Rope rope = ropeGameObject.GetComponent<Rope> ();
		rope.CreateRope (GetMaxBalloonRopeDistance (pBalloon), pBalloon.transform.position, pLineRenderer);
		return rope;
	}
	
	private float GetMaxBalloonRopeDistance(GameObject pBalloonObject)
	{
		Balloon balloon = pBalloonObject.GetComponent<Balloon> ();

		return balloon.m_MaxRopeDistance;
	}
	
	public void AttachRope(GameObject pBalloonObject, Rope pRope, GameObject pTack)
	{
		Rigidbody2D tackBody = pTack.GetComponent<Rigidbody2D> ();
		AttachRopeToCharacter(pBalloonObject, pRope, tackBody);
		AttachRopeToBalloon(pBalloonObject, pRope);
	}
	
	private void AttachRopeToCharacter(GameObject pBalloonObject, Rope pRope, Rigidbody2D pTackBody)
	{
		pRope.GetStartOfRope().GetComponent<HingeJoint2D>().connectedBody = pTackBody;
		DistanceJoint2D balloonJoint= pBalloonObject.GetComponent<DistanceJoint2D> ();
		balloonJoint.distance = GetMaxBalloonRopeDistance (pBalloonObject);
		balloonJoint.connectedBody = pTackBody;
	}
	
	private void AttachRopeToBalloon(GameObject pBalloon, Rope pRope)
	{
		HingeJoint2D balloonHinge = pBalloon.GetComponent<HingeJoint2D> ();
		balloonHinge.connectedBody = pRope.GetEndOfRope().GetComponent<Rigidbody2D>();
		balloonHinge.connectedAnchor = new Vector2 (0, pRope.GetLengthOfEachSegment());
		pRope.transform.parent = pBalloon.transform;
	}

	private void OnAttachBalloon(Enum pEvent, object pBalloon, object pTack)
	{
		GameObject balloonObject = ((Balloon)pBalloon).GameObject;
		Rope rope = balloonObject.transform.GetChild(0).GetComponent<Rope>();
		Rigidbody2D tackBody = ((GameObject)pTack).GetComponent<Rigidbody2D> ();
		AttachRopeToCharacter (balloonObject, rope, tackBody);
	}
}
