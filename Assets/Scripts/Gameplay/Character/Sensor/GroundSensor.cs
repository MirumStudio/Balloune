using UnityEngine;
using System.Collections;

public class GroundSensor : CharacterSensor {

	void Update () {

        Vector2 left = GetBottomLeftCorner();
        Vector2 right = GetBottomRightCorner();

        bool grounded = CheckGround(left, right);
		
        UpdateGroundParamater(grounded);

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

    private void UpdateGroundParamater(bool pValue)
    {
        mAnimator.SetBool(GROUND_PARAMATER, pValue);
    }
}
