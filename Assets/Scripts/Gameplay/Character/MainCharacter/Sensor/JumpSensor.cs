using UnityEngine;
using System.Collections;
using Radix.Event;

public class JumpSensor : CharacterSensor {

	private float AJUST_X = 1f;

	// Update is called once per frame
	void Update () {
		if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
		{
			float speed = mAnimator.GetFloat(SPEED_PARAMTER);

			Vector2 bottom;
			Vector2 top;

			if(speed < 0)
			{
				//CheckLeft();
				bottom = GetBottomLeftCorner();
				top = GetTopLeftCorner();
				bottom.x -= AJUST_X;
				top.x -= AJUST_X;

				Check(bottom, top);

			}
			else if(speed > 0)
			{
				bottom = GetBottomRightCorner();
				top = GetTopRightCorner();
				bottom.x += AJUST_X;
				top.x += AJUST_X;
				
				Check(bottom, top);
			}
		}
		
	}

	private void CheckRight()
	{

	}

	private void CheckLeft()
	{
		Vector2 vector = GetBottomLeftCorner();
		vector.y += 0.05f;
		Debug.DrawLine(GetTopLeftCorner(), vector, Color.green);
		Physics2D.Linecast(GetTopLeftCorner(), vector, GroundLayerMask);
	}

	private void Check(Vector2 mBottom, Vector2 mTop)
	{
		mBottom.y += 0.2f;
		Debug.DrawLine (mTop, mBottom, Color.green);
		if(Physics2D.Linecast(mTop, mBottom, GroundLayerMask))
		{
			Debug.Log("DETECT");
			mAnimator.SetBool("HaveToJump", true);
		}
	}

	#region GetInformation
	private Vector2 GetTopRightCorner()
	{
		return new Vector2(GetRightEdge(), GetTopEdge());
	}
	
	private Vector2 GetTopLeftCorner()
	{
		return new Vector2(GetLeftEdge(), GetTopEdge());
	}
	
	private Vector2 GetBottomRightCorner()
	{
		return new Vector2(GetRightEdge(), GetBottomEdge());
	}
	
	private Vector2 GetBottomLeftCorner()
	{
		return new Vector2(GetLeftEdge(), GetBottomEdge());
	}
	
	private float GetTopEdge()
	{
		return GetCenter().y + mHeight / 2;
	}
	
	private float GetBottomEdge()
	{
		return GetCenter().y - mHeight / 2 - 0.05f;
	}
	
	private float GetLeftEdge()
	{
		return GetCenter().x - mWidth / 2 - 0.1f;
	}
	
	private float GetRightEdge()
	{
		return GetCenter().x + mWidth / 2 + 0.1f;
	}
	
	private Vector2 GetCenter()
	{
		return mCollider.bounds.center;
	}
	
	private int GroundLayerMask
	{
		get
		{
			return 1 << LayerMask.NameToLayer(GROUND_LAYER_NAME);
		}
	}

	#endregion
}

