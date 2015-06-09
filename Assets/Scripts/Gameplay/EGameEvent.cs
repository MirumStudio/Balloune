/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

public enum EGameEvent 
{
    CHILD_COLLISION,
    BALLOON_GIVEN,
	BALLOON_TAKEN,
    LEVEL_FINISHED,
    HAZARDOUS_COLLISION,
    GAME_OVER,
    POPUP_DISPLAYED,
    POPUP_HIDED,
    DISPLAY_PAUSE_POPUP,
    INFLATE_BALLOON,
    BEGIN_PULLING,
    END_PULLING,
	PICKUP_BALLOON,
	DROP_BALLOON,
	ATTEMPT_ATTACH_BALLOON,
	ATTACH_BALLOON,
	DETACH_BALLOON,
	TRIGGER_BALLOON
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

public enum EGameTrigger
{
	LEVEL_END_REACHED
}
