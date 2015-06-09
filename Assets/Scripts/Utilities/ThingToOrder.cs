/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

//TODO: find a better place for this utility function
public static class ThingToOrder {

	public static bool IsBetweenExclusively(this int value, int min, int max)
    {
        return value > min && value < max;
    }

    public static bool IsBetweenInclusively(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }

    public static bool IsBetweenExclusively(this float value, float min, float max)
    {
        return value > min && value < max;
    }

    public static bool IsBetweenInclusively(this float value, float min, float max)
    {
        return value >= min && value <= max;
    }
}
