using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class TouchUtility
{
    public static bool IsNull(this Touch pTouch)
    {
        return pTouch.tapCount == 0 && pTouch.deltaTime == 0f;
    }

    public static bool IsMultiTap(this Touch pTouch)
    {
        return pTouch.tapCount > 1;
    }
}

