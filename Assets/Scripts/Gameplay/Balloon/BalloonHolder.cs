using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;
using System;

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

        EventListener.Register(EGameEvent.INFLATE_BALLOON, OnInfluateBalloon);

        for (int i = 0; i < NUMBER_LIFE_BALLOON; i++)
        {
            CreateBalloune();
		}
	}

    private void SetBalloonBehavior(GameObject pBalloon, BalloonBehavior pBehavior, int pBalloonIndex)
	{
        pBehavior.SetBalloonHolder(this);
        pBehavior.mBalloonIndex = pBalloonIndex;
        pBehavior.SetTack(m_Tack);
	}

	public void DestroyBalloon(int pBalloonIndex) {
		/*Destroy (mLifeBalloons [pBalloonIndex]);
		Destroy (mLifeBalloonsBehavior [pBalloonIndex]);
		mHeldBalloons--;
        for (int i = 0; i < NUMBER_LIFE_BALLOON; i++)
        {
			if(mLifeBalloons[i] != null) {
				mLifeBalloonsBehavior[i].SetInvulnerable(true);
			}
		}*/
	}

    public void CreateBalloune()
    {
		RopeManager ropeManager = new RopeManager (m_RopePrefab, m_Tack);

		var balloonObject = BalloonFactory.CreateBalloon (new Vector2(m_Tack.transform.position.x - (1 * 3), 1.2f));

        var balloon = balloonObject.AddComponent<LifeBalloon>();
        balloon.Init();
        var behavior = balloonObject.GetComponent<BalloonBehavior>();
        SetBalloonBehavior(balloonObject, behavior, mBalloons.Count);

        var rope = ropeManager.CreateRopeForBalloon(balloonObject);
        ropeManager.AttachRope(balloonObject, rope);

        mBalloons.Add(balloon);
    }

	public int CountBalloons() 
    {
		return mBalloons.Count;
	}

    private void OnInfluateBalloon(Enum pEvent, object pArg)
    {
        CreateBalloune();
    }

    public List<Balloon> Ballounes
    {
        get { return mBalloons; }
    }
}
