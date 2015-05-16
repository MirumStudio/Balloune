using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;
using Radix.Utlities;

public class BalloonHolder : MonoBehaviour {
	public const string BALLOON_HOLDER_NAME = "BalloonHolder";
    public const int NUMBER_LIFE_BALLOON = 3;

    [SerializeField]
    protected uint m_MaxBalloonCount = 6;

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	private List<Balloon> mBalloons = new List<Balloon>();

	void Start () {
        BalloonFactory.Init(m_BalloonPrefab, m_Tack);

        EventListener.Register(EGameEvent.INFLATE_BALLOON, OnInflateBalloon);

        for (int i = 0; i < NUMBER_LIFE_BALLOON; i++)
        {
            CreateBalloon(EBalloonType.LIFE);
		}
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
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }

        if (GetLifeBalloonCount() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
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
			}
			
			balloon.Init();
			var physics = balloonObject.GetComponent<BalloonPhysics>();
			SetBalloonProperties(balloon, physics, mBalloons.Count);

			var rope = ropeManager.CreateRopeForBalloon(balloonObject);
			ropeManager.AttachRope(balloonObject, rope);

			mBalloons.Add(balloon);
		}
    }

	public void DetachBalloon(int pBalloonToDetach)
	{
		mBalloons.RemoveAt (pBalloonToDetach);
	}

	public int CountBalloons() 
    {
		return mBalloons.Count;
	}

    private void OnInflateBalloon(Enum pEvent, object pArg)
    {
        var type = EnumUtility.ObjectToEnum<EBalloonType>(pArg);

        CreateBalloon(type);
    }

    public List<Balloon> Ballounes
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
}
