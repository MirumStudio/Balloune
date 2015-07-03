/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public class Direction
{
    private float mDirection;

    public Direction(float pDirection)
    {
        mDirection = pDirection;
    }

    public float Value
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
