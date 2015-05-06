using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_PrefabBalloune;

	[SerializeField]
	protected GameObject m_PrefabRope;

	[SerializeField]
	int m_NumberOfHinges = 4;

	private GameObject[] m_LifeBalloons = new GameObject[3];
	private int heldBalloons = 0;

	// Use this for initialization
	void Start () {
		int firstBalloonX = -32;
		for (int i = 0; i < m_LifeBalloons.Length; i++) {
			m_LifeBalloons[i] = CreateBalloon (firstBalloonX - (i * 3), 1.2f);
			Rope newRope = CreateRopeForBalloon(m_LifeBalloons[i]);
			AttachRope (m_LifeBalloons[i], newRope);
			SetBalloonBehavior(m_LifeBalloons[i], i);
			i = m_LifeBalloons.Length;
		}
	}

    private GameObject CreateBalloon(float x, float y)
	{
		GameObject balloon = Instantiate(m_PrefabBalloune, new Vector2(x, y), Quaternion.identity) as GameObject;
		heldBalloons++;
		return balloon;
    }

	private Rope CreateRopeForBalloon(GameObject pBalloon)
	{
		float maxBalloonDistance = SetMaxBalloonDistance (pBalloon);
		GameObject ropeGameObject = Instantiate (m_PrefabRope, new Vector2 (pBalloon.transform.position.x, 3), Quaternion.identity) as GameObject;
		Rope rope = ropeGameObject.GetComponent<Rope> ();
		rope.createRope (maxBalloonDistance, m_NumberOfHinges);
		return rope;
	}

	private float SetMaxBalloonDistance(GameObject pBalloon)
	{
		DistanceJoint2D balloonJoint= pBalloon.GetComponent<DistanceJoint2D> ();
		balloonJoint.connectedBody = m_Tack.GetComponent<Rigidbody2D>();
		return balloonJoint.distance;
	}

	private void AttachRope(GameObject pBalloon, Rope pRope)
	{
		AttachRopeToCharacter(pRope);
		AttachRopeToBalloon(pBalloon, pRope);
	}

	private void AttachRopeToCharacter(Rope pRope)
	{
		pRope.GetStartOfRope().GetComponent<HingeJoint2D>().connectedBody = m_Tack.GetComponent<Rigidbody2D> ();
	}

	private void AttachRopeToBalloon(GameObject pBalloon, Rope pRope)
	{
		HingeJoint2D balloonHinge = pBalloon.GetComponent<HingeJoint2D> ();
		balloonHinge.connectedBody = pRope.GetEndOfRope().GetComponent<Rigidbody2D>();
		balloonHinge.connectedAnchor = new Vector2 (0, pRope.GetLengthOfEachSegment());
	}

	private void SetBalloonBehavior(GameObject pBalloon, int pBalloonIndex)
	{
		BalloonBehavior balloonBehavior = pBalloon.GetComponent<BalloonBehavior>();
		balloonBehavior.m_Parent = m_Tack.transform;
		balloonBehavior.balloonIndex = pBalloonIndex;
	}

	public void destroyBalloon(int pBalloonIndex) {
		Destroy (m_LifeBalloons [pBalloonIndex]);
		heldBalloons--;
		for(int i = 0; i < m_LifeBalloons.Length; i++) {
			if(m_LifeBalloons[i] != null) {
				m_LifeBalloons[i].GetComponent<BalloonBehavior>().setInvulnerable(true);
			}
		}
	}

	public int countBalloons() {
		return heldBalloons;
	}

	void Update () {
	
	}
}
