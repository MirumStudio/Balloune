using UnityEngine;
using System.Collections;
using Radix.Logging;

public class FuryAttackingState : CharacterState {

    private float mWaitingTime = 0.5f;

    private float mCurrentWaitingTime = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }


    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (animator.GetBool(GROUND_PARAMATER))
        {
            mCurrentWaitingTime += Time.deltaTime;
            if(mCurrentWaitingTime > mWaitingTime)
            {
                mCurrentWaitingTime = 0;
                mBody.AddForce(new Vector2(100 * animator.GetFloat(SPEED_PARAMATER), 700F));
            }
        } else
        {
            mCurrentWaitingTime = 0;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
        mCurrentWaitingTime = 2;
    }
}
