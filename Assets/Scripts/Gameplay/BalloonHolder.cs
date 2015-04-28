using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_PrefabBalloune;

	private GameObject[] m_LifeBalloons = new GameObject[3];
	private int heldBalloons = 0;

	// Use this for initialization
	void Start () {
		int firstBalloonX = -32;
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
        var balloon = Instantiate(m_PrefabBalloune, new Vector2(x, 3), Quaternion.identity) as GameObject;
		DistanceJoint2D balloonJoint= balloon.GetComponent<DistanceJoint2D> ();
		balloonJoint.connectedBody = m_Tack.GetComponent<Rigidbody2D>();
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
