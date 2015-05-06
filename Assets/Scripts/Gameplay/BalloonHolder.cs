using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_BalloonPrefab;

	[SerializeField]
	protected GameObject m_RopePrefab;

	private GameObject[] m_LifeBalloons = new GameObject[3];
	private int heldBalloons = 0;

	void Start () {
		int firstBalloonX = -32;
		BalloonCreator balloonCreator = new BalloonCreator (m_BalloonPrefab, m_RopePrefab, m_Tack);
		for (int i = 0; i < m_LifeBalloons.Length; i++) {
			m_LifeBalloons[i] = balloonCreator.CreateBalloon (new Vector2(firstBalloonX - (i * 3), 1.2f));
			heldBalloons++;
			SetBalloonBehavior(m_LifeBalloons[i], i);
			i = m_LifeBalloons.Length;
		}
	}
	
	private void SetBalloonBehavior(GameObject pBalloon, int pBalloonIndex)
	{
		BalloonBehavior balloonBehavior = pBalloon.GetComponent<BalloonBehavior>();
		balloonBehavior.m_Parent = m_Tack.transform;
		balloonBehavior.balloonIndex = pBalloonIndex;
		balloonBehavior.SetBalloonHolder (this);
	}

	public void DestroyBalloon(int pBalloonIndex) {
		Destroy (m_LifeBalloons [pBalloonIndex]);
		heldBalloons--;
		for(int i = 0; i < m_LifeBalloons.Length; i++) {
			if(m_LifeBalloons[i] != null) {
				m_LifeBalloons[i].GetComponent<BalloonBehavior>().SetInvulnerable(true);
			}
		}
	}

	public int CountBalloons() {
		return heldBalloons;
	}

	void Update () {
	
	}
}
