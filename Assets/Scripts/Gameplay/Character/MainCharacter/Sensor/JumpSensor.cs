using UnityEngine;
using System.Collections;
using Radix.Event;

public class JumpSensor : CharacterSensor {

	private float AJUST_X = 1f;

	// Update is called once per frame
	void FixedUpdate () {
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
		mBottom.y += 0.3f;
		Debug.DrawLine (mTop, mBottom, Color.green);
		if(Physics2D.Linecast(mTop, mBottom, GroundLayerMask))
		{
			mAnimator.SetBool("HaveToJump", true);
		}
	}


}

