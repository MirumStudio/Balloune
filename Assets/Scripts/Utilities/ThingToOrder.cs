using UnityEngine;
using System.Collections;

public static class ThingToOrder {

	public static bool IsBetweenExclusively(this int value, int min, int max)
    {
        return value > min && value < max;
    }

    public static bool IsBetweenInclusively(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }
}
