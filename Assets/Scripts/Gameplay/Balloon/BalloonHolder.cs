using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class BalloonHolder : MonoBehaviour {
	public const string GIRL_BALLOON_HOLDER_NAME = "BalloonHolder";

    [SerializeField]
    protected uint m_MaxBalloonCount = 6;

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	[SerializeField]
	private List<Balloon> mBalloons = new List<Balloon>();

	private GameObject mOwner = null;

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
        mBalloons.Remove(pBalloon);
        Destroy(pBalloon.gameObject);
        Destroy(pBalloon.Physics);
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

    public void CreateBalloon(EBalloonType pType)
    {
		if(mBalloons.Count < m_MaxBalloonCount)
		{
			RopeManager ropeManager = new RopeManager (m_RopePrefab, m_Tack);

			var balloonObject = BalloonFactory.CreateBalloon (new Vector2(m_Tack.transform.position.x - (1 * 3), 1.2f));

			Balloon balloon = null;
			//TODO: Do it in a better way
			switch(pType)
			{
				case EBalloonType.LIFE : 
					{
						balloon  = balloonObject.AddComponent<LifeBalloon>();
						break;
					}
				case EBalloonType.TOXIC:
					{
						balloon = balloonObject.AddComponent<ToxicBalloon>();
						break;
					}
				case EBalloonType.FLYING:
				{
					balloon = balloonObject.AddComponent<FlyingBalloon>();
					break;
				}
			}
			
			balloon.Init();
			var physics = balloonObject.GetComponent<BalloonPhysics>();
			SetBalloonProperties(balloon, physics, mBalloons.Count);

			var rope = ropeManager.CreateRopeForBalloon(balloonObject);
			ropeManager.AttachRope(balloonObject, rope, m_Tack);

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

	private void AttachBalloon(Balloon pBalloon)
	{
		pBalloon.SetBalloonIndex (CountBalloons ());
		pBalloon.SetBalloonHolder (this);
		mBalloons.Add (pBalloon);
		pBalloon.GameObject.transform.parent = this.gameObject.transform;
	}

	public void DetachBalloon(int pBalloonToDetach)
	{
		mBalloons.RemoveAt (pBalloonToDetach);
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
}
