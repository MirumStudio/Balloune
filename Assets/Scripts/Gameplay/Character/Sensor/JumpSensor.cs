using UnityEngine;
using System.Collections;
using Radix.Event;

public class JumpSensor : CharacterSensor {

	private float AJUST_X = 1f;

	void FixedUpdate () {
		if(IsInMovingState())
		{
			Vector2 bottom = Vector2.zero;
			Vector2 top = Vector2.zero;

            GetVector(out bottom, out top);
            Check(bottom, top);
		}
	}

    private void GetVector(out Vector2 pBottom, out Vector2 pTop)
    {
        float speed = GetSpeedParamater();  
        
        if(speed < 0)
        {
            pBottom = GetBottomLeftCorner();
            pTop = GetTopLeftCorner();
        }
        else
        {
            pBottom = GetBottomRightCorner();
            pTop = GetTopRightCorner();
        }

        pBottom.x += AJUST_X * Mathf.Sign(speed);
        pTop.x += AJUST_X * Mathf.Sign(speed);
        pTop.y += 0.75f;
        pBottom.y += 0.4f;
    }

	private void Check(Vector2 pBottom, Vector2 pTop)
	{
        Debug.DrawLine (pTop, pBottom, Color.green);

        if(HaveObstacle(pBottom, pTop)
           && !IsWall(pTop) && HaveMinimalDistance(pBottom))
		{
			mAnimator.SetBool(JUMP_PARAMATER, true);
		}
	}

    private bool HaveObstacle(Vector2 pBottom, Vector2 pTop)
    {
        return Physics2D.Linecast(pTop, pBottom, GroundLayerMask);
    }

    private bool IsWall(Vector2 pTop)
    {
        Vector2 wall = pTop;
        wall.y += 2f;

        Debug.DrawLine (pTop, wall, Color.yellow);

        return Physics2D.Linecast(pTop, wall, GroundLayerMask);
    }

    private bool HaveMinimalDistance(Vector2 pBottom)
    {
        float speed = GetSpeedParamater();  
        Vector2 start = pBottom;
        start.x -= AJUST_X * Mathf.Sign(speed);

        return true;//Physics2D.Linecast(start, pBottom, GroundLayerMask).distance > 0;
    }
}

