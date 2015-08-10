using UnityEngine;
using System.Collections;

public class GirlJumpingState : JumpingState {

    override protected void UpdateJumping() 
    {
        if (mBody.velocity.y < 0.01 && mBody.velocity.y > -0.01)
        {
            float speed = mAnimator.GetFloat(SPEED_PARAMATER);
            
            float xForce = GetXAxisForce();
            
            mBody.AddForce(new Vector2 (xForce, m_JumpForce), ForceMode2D.Force);
        }
    }

    private float GetXAxisForce()
    {
        float xForce =50f;
        if(mAnimator.GetBool(PLATFORM_PARAMETER))
        {
            xForce = 0f;
        }
        if (mAnimator.GetBool(HOLE_PARAMATER))
        {
            xForce = 150f;
            
            Vector2 newVelocity = mBody.velocity;
            newVelocity.x =  Mathf.Sign(mAnimator.GetFloat("Speed")) * 7f;
            mBody.velocity = newVelocity;
        }
        
        xForce *= Mathf.Sign(mAnimator.GetFloat("Speed"));
        
        return xForce;
    }
}
