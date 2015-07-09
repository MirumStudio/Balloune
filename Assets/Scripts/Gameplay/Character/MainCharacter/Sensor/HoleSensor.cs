using UnityEngine;
using System.Collections;

public class HoleSensor : CharacterSensor {

	private float AJUST_X = 0.15f;

	void FixedUpdate () {

		if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
		{
			float speed = mAnimator.GetFloat(SPEED_PARAMTER);
			
			Vector2 right;
			Vector2 left;
			
			if(speed < 0)
			{
				left = GetBottomLeftCorner();
				//bottom.x -= AJUST_X;
				//top.x -= AJUST_X;
				left.x -= AJUST_X;
				Check(left);
				
			}
			else if(speed > 0)
			{
				right = GetBottomRightCorner();
				//bottom.x += AJUST_X;
				//top.x += AJUST_X;
				right.x += AJUST_X;
				Check(right);
			}
		}

		//mAnimator.SetBool ("IsGrounded", grounded);
	}

		private void Check(Vector2 pPoint)
		{
		float speed = mAnimator.GetFloat(SPEED_PARAMTER);
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

			Vector2 other2 = pPoint;
			if(speed < 0)
			{
				other2.x -= 3f;
			}
			else if(speed > 0)
			{
				other2.x += 3f;
			}

		Debug.DrawLine (pPoint, other2, Color.cyan);
		}
}
