using UnityEngine;
using System.Collections;

public class BalloonHolder : MonoBehaviour {

    [SerializeField]
	protected GameObject m_Tack;

	[SerializeField]
    protected GameObject m_PrefabBalloune;

	// Use this for initialization
	void Start () {
        CreateBalloon(-32);
        CreateBalloon(-35);
        CreateBalloon(-37);
	}

    private void CreateBalloon(float x)
    {
        var balloon = Instantiate(m_PrefabBalloune, new Vector2(x, 3), Quaternion.identity) as GameObject;
		SpringJoint2D balloonJoint= balloon.GetComponent<SpringJoint2D> ();
		balloonJoint.connectedBody = m_Tack.GetComponent<Rigidbody2D>();
		//balloonJoint.distance
        balloon.GetComponent<BalloonBehavior>().m_Parent = m_Tack.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
