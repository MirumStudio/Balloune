using UnityEngine;
using System.Collections;

public enum EGameEvent 
{
    CHILD_COLLISION,
    BALLOON_GIVEN,
    LEVEL_FINISHED,
    HAZARDOUS_COLLISION,
    LIFE_COLLISION,
    GAME_OVER,
    POPUP_DISPLAYED,
    POPUP_HIDED,
    DISPLAY_PAUSE_POPUP,
    INFLATE_BALLOON,
    BEGIN_PULLING,
    END_PULLING
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
