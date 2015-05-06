using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	private GameObject[] mLifeBalloons = new GameObject[3];
	private int mHeldBalloons = 0;

	void Start () {
		int firstBalloonX = -32;
		BalloonCreator balloonCreator = new BalloonCreator (m_BalloonPrefab, m_RopePrefab, m_Tack);
		for (int i = 0; i < mLifeBalloons.Length; i++) {
			mLifeBalloons[i] = balloonCreator.CreateBalloon (new Vector2(firstBalloonX - (i * 3), 1.2f));
			mHeldBalloons++;
			SetBalloonBehavior(mLifeBalloons[i], i);
			i = mLifeBalloons.Length;
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

	void Update () {
	
	}
}
