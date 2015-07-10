using UnityEngine;
using System.Collections;

public class JumpingState : CharacterState {
	[SerializeField]
	protected float m_JumpForce = 1000f;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);

		if (mBody.velocity.y == 0) 
        {
            float xForce = GetXAxisForce(animator);

			mBody.AddForce(new Vector2 (xForce, m_JumpForce));
		}

        ReinitializeJumpParamater(animator);
        ReinitializePlateformParamater(animator);
	}

    private float GetXAxisForce(Animator animator)
    {
        float xForce = 50f;
        if(animator.GetBool(PLATEFORM_PARAMATER))
        {
            xForce = 0f;
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
        animator.SetBool(JUMP_PARAMATER, false);
    }
}
