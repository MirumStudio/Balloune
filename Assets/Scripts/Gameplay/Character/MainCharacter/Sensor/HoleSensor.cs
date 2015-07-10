using UnityEngine;
using System.Collections;

public class HoleSensor : CharacterSensor {

	private float AJUST_X = 0.15f;

	void FixedUpdate () {

		if(IsInMovingState())
		{
			float speed = mAnimator.GetFloat(SPEED_PARAMATER);
			
			Vector2 right;
			Vector2 left;
			
			if(speed < 0)
			{
				left = GetBottomLeftCorner();
				left.x -= AJUST_X;
				Check(left);
				
			}
			else if(speed > 0)
			{
				right = GetBottomRightCorner();
				right.x += AJUST_X;
				Check(right);
			}
		}
	}

	private void Check(Vector2 pPoint)
	{
	    float speed = mAnimator.GetFloat(SPEED_PARAMATER);
		Vector2 bot = pPoint;
		bot.y -= 3f;
		Debug.DrawLine (pPoint, bot, Color.cyan);

		if(!Physics2D.Linecast(pPoint, bot, GroundLayerMask))
		{
			bool haveToJump = false;
			Vector2 other = pPoint;
			if(speed < 0)
			{
				other.x -= 3f;
			}
			else if(speed > 0)
			{
				other.x += 3f;
			}
			haveToJump = Physics2D.Linecast(pPoint, other, GroundLayerMask);
			mAnimator.SetBool("HaveToJump", haveToJump);
		}
	}
}
