using UnityEngine;
using System.Collections;

public class MovingState : CharacterState {

	protected bool m_IsFacingRight = true;

    [SerializeField]
	private float m_MaxSpeed = 4f;

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		float speed = animator.GetFloat(SPEED_PARAMATER);

		Direction direction = GetDirection(speed);
		
		if (speed != 0  && !HorizontalMaxSpeedReached(direction))
		{
            Move(speed);
		}

		CheckFlipping(direction);
	}

    private void Move(float speed)
    {
        Vector2 newVelocity = mBody.velocity;
        newVelocity.x = Mathf.Sign(speed) * m_MaxSpeed;
        mBody.velocity = newVelocity;
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
