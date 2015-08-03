using UnityEngine;
using System.Collections;

public class GroundSensor : CharacterSensor {

    private bool mIsGrounded = false;

    protected override void Start()
    {
        base.Start();
        UpdateGroundParamater();
    }

	void Update () {

        Vector2 left = GetBottomLeftCorner();
        Vector2 right = GetBottomRightCorner();

       /* left.x += 0.3f;
            right.x -= 0.3f;*/

        bool grounded = CheckGround(left, right);
		
        if (mIsGrounded != grounded)
        {
            mIsGrounded = grounded;
            UpdateGroundParamater();
        }

        DrawDebugLine(left, right);
	}

    private void DrawDebugLine(Vector2 pLeft, Vector2 pRight)
    {
        Debug.DrawLine(pLeft, pRight, Color.red);
    }

    private bool CheckGround(Vector2 pLeft, Vector2 pRight)
    {
        return Physics2D.Linecast(pLeft, pRight, GroundLayerMask)
            || Physics2D.Linecast(pLeft, pRight, PlateformLayerMask);
    }

    private void UpdateGroundParamater()
    {
        mAnimator.SetBool(GROUND_PARAMATER, mIsGrounded);
    }
}
