using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {
	private const int NUMBER_OF_BALLOONS = 3;
    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	private GameObject[] mLifeBalloons = new GameObject[NUMBER_OF_BALLOONS];
	private Rope[] mRopes = new Rope[NUMBER_OF_BALLOONS];

	private int mHeldBalloons = 0;

	void Start () {
		int firstBalloonX = -32;
		BalloonCreator balloonCreator = new BalloonCreator (m_BalloonPrefab, m_Tack);
		RopeManager ropeManager = new RopeManager (m_RopePrefab, m_Tack);
		Debug.Log (mLifeBalloons.Length);
		for (int i = 0; i < mLifeBalloons.Length; i++) {
			mLifeBalloons[i] = balloonCreator.CreateBalloon (new Vector2(firstBalloonX - (i * 3), 1.2f));
			mHeldBalloons++;
			SetBalloonBehavior(mLifeBalloons[i], i);
			mRopes[i] = ropeManager.CreateRopeForBalloon (mLifeBalloons[i]);
			ropeManager.AttachRope(mLifeBalloons[i], mRopes[i]);
		}
	}

	private void SetBalloonBehavior(GameObject pBalloon, int pBalloonIndex)
	{
		BalloonBehavior balloonBehavior = pBalloon.GetComponent<BalloonBehavior>();
		balloonBehavior.m_Parent = m_Tack.transform;
		balloonBehavior.mBalloonIndex = pBalloonIndex;
		balloonBehavior.SetBalloonHolder (this);
	}

	public void DestroyBalloon(int pBalloonIndex) {
		Destroy (mLifeBalloons [pBalloonIndex]);
		mHeldBalloons--;
		for(int i = 0; i < mLifeBalloons.Length; i++) {
			if(mLifeBalloons[i] != null) {
				mLifeBalloons[i].GetComponent<BalloonBehavior>().SetInvulnerable(true);
			}
		}
	}

	public int CountBalloons() {
		return mHeldBalloons;
	}

	public Rope GetRope(int index)
	{
		return mRopes[index];
	}

	void Update () {
	
	}
}
