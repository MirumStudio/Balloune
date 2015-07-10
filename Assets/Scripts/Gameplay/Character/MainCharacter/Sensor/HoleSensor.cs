using UnityEngine;
using System.Collections;

public class HoleSensor : CharacterSensor {

	private float AJUST_X = 0.15f;

    [SerializeField]
    private float m_HoleMaxLenght = 3f;

    [SerializeField]
    private float m_HoleDeep = 3f;

	void FixedUpdate () {

		if(IsInMovingState())
		{
            Vector2 top = GetTopVector();
			
            CheckHole(top);
		}
	}

    private Vector2 GetTopVector()
    {
        float speed = GetSpeedParamater();

        Vector2 top;
        
        if(speed < 0)
        {
            top = GetBottomLeftCorner();
        }
        else
        {
            top = GetBottomRightCorner();
        }

        top.x += AJUST_X * Mathf.Sign(speed);

        return top;
    }

	private void CheckHole(Vector2 pTop)
	{
        Vector2 bot = pTop;
        bot.y -= m_HoleDeep;

        DrawDebugLine(pTop, bot);

        if(!Physics2D.Linecast(pTop, bot, GroundLayerMask))
		{
            CheckHoleLenght(pTop);
		}
	}

    private void CheckHoleLenght(Vector2 pTop)
    {
        float speed = GetSpeedParamater();
        Vector2 extremity = pTop;
        extremity.x += m_HoleMaxLenght * Mathf.Sign(speed);

        bool haveToJump = Physics2D.Linecast(pTop, extremity, GroundLayerMask);
        mAnimator.SetBool(JUMP_PARAMATER, haveToJump);
    }

    private void DrawDebugLine(Vector2 pTop, Vector2 pBot)
    {
        Debug.DrawLine (pTop, pBot, Color.cyan);
    }
}
