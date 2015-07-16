using UnityEngine;
using System.Collections;

public enum EDirection {

	LEFT = -1,
    RIGHT = 1,
    NONE = 0
}

public static class DirectionUtility{

    public static int Sign(this EDirection pDirection)
    {
        return (int) pDirection;
    }
}