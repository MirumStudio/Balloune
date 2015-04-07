using UnityEngine;
using System.Collections;

/*Prototype code*/

public class BalloonCreator : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_BalloonPrefab;
	[SerializeField]
	private Rigidbody2D m_BalloonAnchor;

	public void createBalloon()
	{
		GameObject balloon = GameObject.Instantiate(m_BalloonPrefab,this.m_BalloonAnchor.transform.position+(Vector3.up*0.3f),this.m_BalloonAnchor.transform.rotation) as GameObject;
		balloon.GetComponent<SpringJoint2D>().connectedBody=this.m_BalloonAnchor;
		balloon.GetComponent<SpriteRenderer>().color=new Color(Random.value*2,Random.value*2,Random.value*2,0.9f);
	}
}
