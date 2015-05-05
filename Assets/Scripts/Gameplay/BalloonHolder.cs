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
		int firstBalloonX = -45;
		for (int i = 0; i < m_LifeBalloons.Length; i++) {
			m_LifeBalloons[i] = CreateBalloon (firstBalloonX - (i * 3));
			BalloonBehavior balloonBehavior = m_LifeBalloons[i].GetComponent<BalloonBehavior>();
			balloonBehavior.m_Parent = m_Tack.transform;
			balloonBehavior.balloonIndex = i;
			i = m_LifeBalloons.Length;
		}
	}

    private GameObject CreateBalloon(float x)
	{
		//Rope rope = new Rope(ropeLength);
		//attach rope to character
		
		GameObject balloon = Instantiate(m_PrefabBalloune, new Vector2(x, 3), Quaternion.identity) as GameObject;
		DistanceJoint2D balloonJoint= balloon.GetComponent<DistanceJoint2D> ();
		balloonJoint.connectedBody = m_Tack.GetComponent<Rigidbody2D>();

		float ropeLength = balloonJoint.distance;
		GameObject ropeGameObject = Instantiate (m_PrefabRope, new Vector2 (x, 3), Quaternion.identity) as GameObject;
		Rope rope = ropeGameObject.GetComponent<Rope> ();
		rope.createRope (ropeLength, m_NumberOfHinges);
		rope.GetStartOfRope().GetComponent<HingeJoint2D>().connectedBody = m_Tack.GetComponent<Rigidbody2D> ();

		//attach balloon to rope
		HingeJoint2D balloonHinge = balloon.GetComponent<HingeJoint2D> ();
		balloonHinge.connectedBody = rope.GetEndOfRope().GetComponent<Rigidbody2D>();
		balloonHinge.connectedAnchor = new Vector2 (0, rope.GetLengthOfEachSegment());

		//rope = balloon.AddComponent<Rope>();
		//Debug.Log ("balloon ropes : " + balloon.GetComponents<Rope>().Length);
		heldBalloons++;
		return balloon;
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
	// Update is called once per frame
	void Update () {
	
	}
}
