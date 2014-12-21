using UnityEngine;
using System.Collections;

public class BaloonBehavior : MonoBehaviour {
	[SerializeField]
	private LineRenderer m_LineRenderer;
	[SerializeField]
	private SpringJoint2D m_SpringJoint;
	[SerializeField]
	private Transform m_RopeStart;
	void Start()
	{
		/*this.m_LineRenderer.sortingLayerName="Background";
		this.m_LineRenderer.sortingOrder=0;*/
	}
	void Update()
	{
		this.m_LineRenderer.SetPosition(0,this.m_RopeStart.transform.position);
		//this.m_LineRenderer.SetPosition(1,this.m_SpringJoint.connectedBody.transform.position);
	}
	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Destroy (this.gameObject);
		}
	}
}
