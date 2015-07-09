using UnityEngine;
using System.Collections;

public class MovingState : CharacterState {

	protected bool m_IsFacingRight = true;
	private float m_MaxSpeed = 4f;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		Debug.Log ("MOVING STATE: Enter");
	}

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//AddForce (Vector2.right * animator.GetFloat("Speed") * 100);

		float speed = animator.GetFloat ("Speed");

		Direction direction = GetDirection (Mathf.Sign(speed));
		
		if (direction.Value != 0 /*&& CanMove(direction)*/ && !HorizontalMaxSpeedReached(direction))
		{
			//AddForce(Vector2.right * speed * 500);

			Vector2 newVelocity = mBody.velocity;

			newVelocity.x = Mathf.Sign(speed) * m_MaxSpeed;
			
			mBody.velocity = newVelocity;
		}
		
		//AjustVelocity();
		CheckFlipping(direction);
	}

	protected bool HorizontalMaxSpeedReached(Direction pDirection)
	{
		return pDirection.Value * mBody.velocity.x >= m_MaxSpeed;
	}

	protected void CheckFlipping(Direction pDirection)
	{
		if (pDirection.IsRightDirection() && !m_IsFacingRight
		    || pDirection.IsLeftDirection() && m_IsFacingRight)
		{
			Flip();
		}
	}

	protected void Flip()
	{
		m_IsFacingRight = !m_IsFacingRight;
		
		Vector3 theScale = mBody.transform.localScale;
		theScale.x *= -1;
		mBody.transform.localScale = theScale;
	}

	protected void AjustVelocity()
	{
		Vector2 newVelocity = mBody.velocity;
		
		if (Mathf.Abs(mBody.velocity.x) > m_MaxSpeed)
		{
			newVelocity.x = Mathf.Sign(mBody.velocity.x) * m_MaxSpeed;
		}
		
		mBody.velocity = newVelocity;
	}

	protected Direction GetDirection(float speed)
	{
		int directionInt = 0;
		if (speed > 0) {
			directionInt = 1;
		} else if (speed < 0) {
			directionInt = -1;
		}
		return new Direction (directionInt);
	}
}
