/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

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

