using UnityEngine;
using System.Collections;

public class FuryJumpingState : JumpingState {

    override protected void UpdateJumping()
    {
        if(!mAnimator.GetBool(PLATFORM_PARAMETER) && !(mAnimator.GetBool(HOLE_PARAMATER)))
        {
            float speed = mAnimator.GetFloat(SPEED_PARAMATER);
            mBody.AddForce(new Vector2 (Mathf.Sign(speed) *100f, m_JumpForce + 150f));
        }
    }
}
