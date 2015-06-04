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
