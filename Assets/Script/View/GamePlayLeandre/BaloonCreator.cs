using UnityEngine;
using System.Collections;

public class BaloonCreator : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_BaloonPrefab;
	[SerializeField]
	private Rigidbody2D m_BaloonAnchor;

	public void createBaloon()
	{
		GameObject baloon = GameObject.Instantiate(m_BaloonPrefab,this.m_BaloonAnchor.transform.position+(Vector3.up*0.3f),this.m_BaloonAnchor.transform.rotation) as GameObject;
		baloon.GetComponent<SpringJoint2D>().connectedBody=this.m_BaloonAnchor;
		baloon.GetComponent<SpriteRenderer>().color=new Color(Random.value*2,Random.value*2,Random.value*2,0.9f);
	}
}
