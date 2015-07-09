using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class CharacterSensor : MonoBehaviour {

	protected const string GROUND_LAYER_NAME = "Ground"; 
	protected const string PLATEFORM_LAYER_NAME = "OneWayGround";

	protected const string IS_PULlING_PARAMTER = "IsPulling";
	protected const string SPEED_PARAMTER = "Speed";
	
	protected float mWidth;
	protected float mHeight;
	
	protected Collider2D mCollider;

	protected Animator mAnimator;

	// Use this for initialization
	virtual protected void Start () {
		mAnimator = GetComponent<Animator> ();

		mCollider = GetComponent<Collider2D>();
		mWidth = mCollider.bounds.size.x;
		mHeight = mCollider.bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region GetInformation
	protected Vector2 GetTopRightCorner()
	{
		return new Vector2(GetRightEdge(), GetTopEdge());
	}
	
	protected Vector2 GetTopLeftCorner()
	{
		return new Vector2(GetLeftEdge(), GetTopEdge());
	}
	
	protected Vector2 GetBottomRightCorner()
	{
		return new Vector2(GetRightEdge(), GetBottomEdge());
	}
	
	protected Vector2 GetBottomLeftCorner()
	{
		return new Vector2(GetLeftEdge(), GetBottomEdge());
	}
	
	protected float GetTopEdge()
	{
		return GetCenter().y + mHeight / 2;
	}
	
	protected float GetBottomEdge()
	{
		return GetCenter().y - mHeight / 2 - 0.05f;
	}
	
	protected float GetLeftEdge()
	{
		return GetCenter().x - mWidth / 2 - 0.1f;
	}
	
	protected float GetRightEdge()
	{
		return GetCenter().x + mWidth / 2 + 0.1f;
	}
	
	protected Vector2 GetCenter()
	{
		return mCollider.bounds.center;
	}
	
	protected int GroundLayerMask
	{
		get
		{
			return 1 << LayerMask.NameToLayer(GROUND_LAYER_NAME);
		}
	}

	protected int PlateformLayerMask
	{
		get
		{
			return 1 << LayerMask.NameToLayer(PLATEFORM_LAYER_NAME);
		}
	}
	
	#endregion
}
