﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class BalloonHolder : MonoBehaviour {
	public const string BALLOON_HOLDER_NAME = "BalloonHolder";

    [SerializeField]
    protected uint m_MaxBalloonCount = 6;

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	protected List<Balloon> mBalloons = new List<Balloon>();

	protected GameObject mOwner = null;

	protected virtual void Start () {
		BalloonFactory.Init(m_BalloonPrefab, m_Tack);
		mOwner = m_Tack.transform.parent.gameObject;
		EventListener.Register(EGameEvent.ATTEMPT_ATTACH_BALLOON, OnAttemptAttachBalloon);
	}
	
	private void SetBalloonProperties(Balloon pBalloon, BalloonPhysics pPhysic, int pBalloonIndex)
	{
        pBalloon.SetBalloonHolder(this);
        pBalloon.SetBalloonIndex (pBalloonIndex);
        pPhysic.SetTack(m_Tack);
	}

    public void DestroyBalloon(Balloon pBalloon)
    {
        DetachBalloon(pBalloon);
        Destroy(pBalloon.gameObject);
        Destroy(pBalloon.Physics);
		EventService.DispatchEvent (EGameEvent.DROP_BALLOON, pBalloon);
        foreach(Balloon balloon in mBalloons)
        {
            balloon.Physics.SetInvulnerable(true);
        }

        if (GetLifeBalloonCount() <= 0)
        {
            EventService.DispatchEvent(EGameEvent.GAME_OVER, null);
        }

        if (GetLifeBalloonCount() <= 0)
        {
            EventService.DispatchEvent(EGameEvent.GAME_OVER, null);
        }
	}

    public virtual void CreateBalloon(EBalloonType pType)
	{
		if(mBalloons.Count < m_MaxBalloonCount)
		{
			RopeManager ropeManager = new RopeManager (m_RopePrefab, m_Tack);

			Vector2 baseBalloonPosition = GetPositionXOffset(m_Tack.transform.position);
			Balloon balloon = BalloonFactory.CreateBalloon (pType, baseBalloonPosition);
			
			GameObject balloonObject = balloon.GameObject;

			var physics = balloonObject.GetComponent<BalloonPhysics>();
			SetBalloonProperties(balloon, physics, mBalloons.Count);

			LineRenderer balloonLineRenderer = balloonObject.GetComponent<LineRenderer>();
			Rope rope = ropeManager.CreateRopeForBalloon(balloonLineRenderer, balloonObject);
			ropeManager.AttachRope(balloonObject, rope, m_Tack);
			balloonObject.transform.parent = this.gameObject.transform;

			mBalloons.Add(balloon);
		}
    }

	public void OnAttemptAttachBalloon(Enum pEvent, object pBalloon, object pPosition)
	{
		if (CountBalloons () < m_MaxBalloonCount) {
			Collider2D[] touchedColliders = Physics2D.OverlapCircleAll ((Vector2)pPosition,  1f);
			Collider2D thisCollider = m_Tack.transform.parent.GetComponent<Collider2D> ();
			for(int i = 0; i < touchedColliders.Length; i++)
			{
				if(touchedColliders[i] == thisCollider)
				{
					AttachBalloon ((Balloon) pBalloon);
					EventService.DispatchEvent(EGameEvent.ATTACH_BALLOON, pBalloon, m_Tack);
					break;
				}
			}
		}
	}

	protected virtual void AttachBalloon(Balloon pBalloon)
	{
		pBalloon.SetBalloonIndex (CountBalloons ());
		pBalloon.SetBalloonHolder (this);
		mBalloons.Add (pBalloon);
		pBalloon.GameObject.transform.parent = this.gameObject.transform;
	}

	public virtual void DetachBalloon(Balloon pBalloonToDetach)
	{
		mBalloons.Remove (pBalloonToDetach);
	}

	public int CountBalloons() 
    {
		return mBalloons.Count;
	}

    public List<Balloon> Balloons
    {
        get { return mBalloons; }
    }

    public int GetLifeBalloonCount()
    {
        return mBalloons.FindAll((balloune) => 
        {
            return balloune is LifeBalloon;
        }).Count;
    }

	public GameObject Owner
	{
		get { return mOwner; }
	}

	private Vector2 GetPositionXOffset(Vector2 pBasePosition)
	{
		Vector2 positionOffset = pBasePosition;
		//TODO remove the division by two when inflated ballons are not automatically given to the girl
		positionOffset.x = positionOffset.x + (mBalloons.Count/2);
		return positionOffset;
	}
}