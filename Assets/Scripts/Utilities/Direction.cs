using UnityEngine;
using System.Collections;

public class Direction
{
    private int mDirection;

    public Direction(int pDirection)
    {
        mDirection = pDirection;
    }

    public int Value
    {
        get { return mDirection; }
    }

    public EEdge Edge
    {
        get
        {
            if(mDirection < 0)
            {
                return EEdge.LEFT;
            }
            else if(mDirection > 0)
            {
                return EEdge.RIGHT;
            }
            else
            {
                return EEdge.NONE;
            }
        }
    }

    public bool IsLeftDirection()
    {
        return Edge == EEdge.LEFT;
    }

    public bool IsRightDirection()
    {
        return Edge == EEdge.RIGHT;
    }
}
