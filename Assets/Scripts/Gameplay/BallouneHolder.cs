using UnityEngine;
using System.Collections;

public class BallouneHolder : MonoBehaviour {

    public GameObject m_Tack;
    public GameObject m_PrefabBalloune;

	// Use this for initialization
	void Start () {
        CreateBalloon();
        CreateBalloon();
        CreateBalloon();
	}

    private void CreateBalloon()
    {
        var balloune = Instantiate(m_PrefabBalloune, new Vector2(-34, 3), Quaternion.identity) as GameObject;
        balloune.transform.parent = transform;

        balloune.GetComponent<SpringJoint2D>().connectedBody = m_Tack.GetComponent<Rigidbody2D>();
        balloune.GetComponent<BalloonBehavior>().mParent = m_Tack.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
