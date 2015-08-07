using UnityEngine;
using System.Collections;
using Radix.Logging;

public class FuryAttackingState : CharacterState {

    private float mWaitingTime = 1;

    private float mCurrentWaitingTime = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }


    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (animator.GetBool(GROUND_PARAMATER))
        {
            Log.Debug(mCurrentWaitingTime.ToString(), ELogCategory.CHARACTER_STATE);
            mCurrentWaitingTime += Time.deltaTime;
            if(mCurrentWaitingTime > mWaitingTime)
            {
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
