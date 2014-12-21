using UnityEngine;
using System.Collections;

public class RectCollider : MonoBehaviour 
{	
	[SerializeField]
	private float m_GroundDistaceFromPivot = 0.1f;
	[SerializeField]
	private float m_GroundedBaseWidth = 0.5f;
	[SerializeField]
	private float m_BodyWidth = 1f;
	[SerializeField]
	private float m_BodyHeight = 1f;
	[SerializeField]
	private float m_FeetHeight = 0.1f;

	private RaycastHit2D mGroundRcH;
	private RaycastHit2D mTopRcH;
	private RaycastHit2D mRightRcH;
	private RaycastHit2D mLeftRcH;
	private Vector2 mPivot;

	public enum Edges{Ground,Left,Right,Top}

	void Update(){
		mPivot = this.transform.position - new Vector3(0, m_GroundDistaceFromPivot, 0);
		this.TopAndBottomHitTests();
		this.SideHitTests();
	}

	//Public Gets
	public bool TouchSomething(Edges pEdge){
		return GetEdgeRayCastHit(pEdge).collider!=null;
	}

	public Collider2D GetHitEntity(Edges pEdge){
		return GetEdgeRayCastHit(pEdge).collider;
	}

	//Utilities
	private RaycastHit2D GetEdgeRayCastHit(Edges pEdge)
	{
		switch(pEdge)
		{
		case Edges.Ground:
			return mGroundRcH;
		case Edges.Left:
			return mLeftRcH;
		case Edges.Right:
			return mRightRcH;
		case Edges.Top:
			return mTopRcH;
		}
		throw new UnityException("No Ray Cast available of this Edge:"+pEdge.ToString());
	}

	private RaycastHit2D RayTest(Vector2 pStartRay,float pRayLength,Vector2 pRayDirection)
	{
		//DEBUG
		Debug.DrawLine(pStartRay,pStartRay + pRayDirection*pRayLength, Color.red);
		return Physics2D.Raycast(pStartRay, pRayDirection,pRayLength);
	}

	//RayCast Tests
	private void TopAndBottomHitTests(){
		Vector2 startRay = mPivot - new Vector2(m_GroundedBaseWidth/2,0);
		float rayLength = m_GroundedBaseWidth;

		mGroundRcH = RayTest(startRay,rayLength,Vector2.right);
		startRay += new Vector2 (0,m_BodyHeight);
		mTopRcH = RayTest(startRay,rayLength,Vector2.right);
	}

	private void SideHitTests()
	{
		Vector2 startRay = mPivot - new Vector2(m_BodyWidth/2,-m_BodyHeight);
		float rayLength = m_BodyHeight-m_FeetHeight;

		mLeftRcH = RayTest(startRay,rayLength,-Vector2.up);
		startRay+=new Vector2(m_BodyWidth,0);
		mRightRcH = RayTest(startRay,rayLength,-Vector2.up);
	}
}
