using UnityEngine;
using System.Collections;

public abstract class JumpingState : CharacterState {

	protected float m_JumpForce = 800f;
    protected Animator mAnimator;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
       
        mAnimator = animator;

        UpdateJumping();

        ReinitializeJumpParamater(animator);
        ReinitializePlateformParamater(animator);
        ReinitializePHoleParamater(animator);
	}

    protected abstract void UpdateJumping();

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
