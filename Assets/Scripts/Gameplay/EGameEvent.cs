﻿using UnityEngine;
using System.Collections;

public enum EGameEvent 
{
    CHILD_COLLISION,
    BALLOON_GIVEN,
    LEVEL_FINISHED,
    HAZARDOUS_COLLISION,
    LIFE_COLLISION,
    GAME_OVER
}

public enum EGameControl
{
    JUMP_PRESSED,
    JUMP_UP,
    LEFT_PRESSED,
    LEFT_UP,
    RIGHT_PRESSED,
    RIGHT_UP
}
