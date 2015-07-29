using UnityEngine;
using System.Collections;

public class JumpingState : CharacterState {

	protected float m_JumpForce = 800f;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
       

        if (mBody.velocity.y < 0.01 && mBody.velocity.y > -0.01)
        {
            float speed = animator.GetFloat(SPEED_PARAMATER);

            float xForce = GetXAxisForce(animator);
			



            mBody.AddForce(new Vector2 (xForce, m_JumpForce));
		}

        ReinitializeJumpParamater(animator);
        ReinitializePlateformParamater(animator);
        ReinitializePHoleParamater(animator);
	}

    private float GetXAxisForce(Animator animator)
    {
        float xForce =50f;
        if(animator.GetBool(PLATFORM_PARAMETER))
        {
            xForce = 0f;
        }
        if (animator.GetBool(HOLE_PARAMATER))
        {
            xForce = 150f;

            Vector2 newVelocity = mBody.velocity;
            newVelocity.x =  Mathf.Sign(animator.GetFloat("Speed")) * 7f;
            mBody.velocity = newVelocity;
        }

        xForce *= Mathf.Sign(animator.GetFloat("Speed"));

        return xForce;
    }

    private void ReinitializeJumpParamater(Animator animator)
    {
        animator.SetBool(JUMP_PARAMATER, false);
    }

    private void ReinitializePlateformParamater(Animator animator)
    {
        animator.SetBool(PLATFORM_PARAMETER, false);
    }

    private void ReinitializePHoleParamater(Animator animator)
    {
        animator.SetBool(HOLE_PARAMATER, false);
    }
}
