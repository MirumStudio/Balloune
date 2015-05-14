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

	private List<GameObject> mLifeBalloons = new List<GameObject>();
	private List<BalloonBehavior> mLifeBalloonsBehavior = new  List<BalloonBehavior>();
	private List<Rope> mRopes = new List<Rope>();

	private int mHeldBalloons = 0;

	void Start () {
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
		Destroy (mLifeBalloons [pBalloonIndex]);
		Destroy (mLifeBalloonsBehavior [pBalloonIndex]);
		mHeldBalloons--;
        for (int i = 0; i < NUMBER_LIFE_BALLOON; i++)
        {
			if(mLifeBalloons[i] != null) {
				mLifeBalloonsBehavior[i].SetInvulnerable(true);
			}
		}
	}

    public void CreateBalloune()
    {
        BalloonCreator balloonCreator = new BalloonCreator (m_BalloonPrefab, m_Tack);
		RopeManager ropeManager = new RopeManager (m_RopePrefab, m_Tack);

		var balloon = balloonCreator.CreateBalloon (new Vector2(m_Tack.transform.position.x - (1 * 3), 1.2f));
		var behavior = balloon.GetComponent<BalloonBehavior>();
        SetBalloonBehavior(balloon, behavior, mHeldBalloons);
        mHeldBalloons++;
		var rope = ropeManager.CreateRopeForBalloon (balloon);
        ropeManager.AttachRope(balloon, rope);

        mRopes.Add(rope);
        mLifeBalloons.Add(balloon);
        mLifeBalloonsBehavior.Add(behavior);
    }

	public int CountBalloons() 
    {
		return mHeldBalloons;
	}

	public Rope GetRope(int index)
	{
		return mRopes[index];
	}

    public List<BalloonBehavior> GetLifeBalloonsBehavior()
    {
        return mLifeBalloonsBehavior;
    }

    private void OnInfluateBalloon(Enum pEvent, object pArg)
    {
        CreateBalloune();
    }
}
