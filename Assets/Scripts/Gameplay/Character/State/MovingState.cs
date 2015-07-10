using UnityEngine;
using System.Collections;

public class MovingState : CharacterState {

	protected bool m_IsFacingRight = true;

    [SerializeField]
	private float m_MaxSpeed = 4f;

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		float speed = animator.GetFloat(SPEED_PARAMATER);

		if (speed != 0)
		{
            Move(speed);
		}

		CheckFlipping(speed);
	}

    private void Move(float speed)
    {
        Vector2 newVelocity = mBody.velocity;
        newVelocity.x = Mathf.Sign(speed) * m_MaxSpeed;
        mBody.velocity = newVelocity;
    }

    protected void CheckFlipping(float speed)
	{
		if (speed > 0 && !m_IsFacingRight
            || speed < 0 && m_IsFacingRight)
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
}
