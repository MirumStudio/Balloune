using UnityEngine;
using System.Collections;

public class BalloonTest : MonoBehaviour {

    public float m_MaxDistance = 4.00f;
    public float m_aDrag = 1f;
    public Transform mParent = null;
    LineRenderer mline = null;
	// Use this for initialization
	void Start () {
        mline = GetComponent<LineRenderer>();
        mline.SetPosition(0, transform.position);
        mline.SetPosition(1, mParent.position);
	}
	
	// Update is called once per frame
	void Update () {

        var body = GetComponent<Rigidbody2D>();

        Debug.Log(body.angularVelocity);
        //body.angularVelocity = 1f;

        float x = body.velocity.x;
        
        if(x > 0)
        {
            x = Mathf.Min(x, 3.0f);
        }
        else if (x < 0)
        {
            x = Mathf.Max(x, -3.0f);
        }

        body.velocity = new Vector2(x, Mathf.Max(-1, body.velocity.y));




        body.angularDrag = m_aDrag;

        mline.SetPosition(0, transform.position);
        mline.SetPosition(1, mParent.position);
        float distance = Vector2.Distance(mParent.position, transform.position);
        var spring = GetComponent<SpringJoint2D>();
        //GetComponent<SpringJoint2D>().distance = Mathf.Min(distance, m_MaxDistance);

        if (distance >= m_MaxDistance)
        {
            GetComponent<SpringJoint2D>().enabled = true;
        }
        else if (distance < m_MaxDistance && GetComponent<SpringJoint2D>().enabled)
        {
            GetComponent<SpringJoint2D>().enabled = false;
        }
	}
}
