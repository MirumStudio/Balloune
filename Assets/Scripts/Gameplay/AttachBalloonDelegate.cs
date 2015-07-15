/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public delegate void InteractableDelegate(Interactable pArg);
public delegate void BalloonDelegate(Balloon pArg);
public delegate void BalloonTypeDelegate(EBalloonType pArg);
public delegate void CharacterPullDelegate(CharacterPull pPull, Balloon pBalloon);
public delegate void AttempAttachBalloonDelegate(Balloon pBalloon, Vector2 pVec);
public delegate void AttachBalloonDelegate(Balloon pBalloon, GameObject pArg);

public enum EGameEvent 
{
    [EventHandlerAttribute(typeof(InteractableDelegate))]
    CHILD_COLLISION,

    [EventHandlerAttribute(typeof(BalloonDelegate))]
    BALLOON_GIVEN,
    
    [EventHandlerAttribute(typeof(BalloonDelegate))]
	BALLOON_TAKEN,
    
    [EventHandlerAttribute()]
    LEVEL_FINISHED,
    
    [EventHandlerAttribute(typeof(InteractableDelegate))]
    HAZARDOUS_COLLISION,
    
    [EventHandlerAttribute()]
    GAME_OVER,
    
    [EventHandlerAttribute(typeof(PopupDelegate))]
    POPUP_DISPLAYED,
    
    [EventHandlerAttribute(typeof(PopupDelegate))]
    POPUP_HIDED,
    
    [EventHandlerAttribute()]
    DISPLAY_PAUSE_POPUP,
    
    [EventHandlerAttribute(typeof(BalloonTypeDelegate))]
    INFLATE_BALLOON,
    
    [EventHandlerAttribute(typeof(CharacterPullDelegate))]
    BEGIN_PULLING,

    [EventHandlerAttribute()]
    END_PULLING,
    
    [EventHandlerAttribute(typeof(BalloonDelegate))]
	PICKUP_BALLOON,
    
    [EventHandlerAttribute(typeof(BalloonDelegate))]
	DROP_BALLOON,
    
    [EventHandlerAttribute(typeof(AttempAttachBalloonDelegate))]
	ATTEMPT_ATTACH_BALLOON,
    
    [EventHandlerAttribute(typeof(AttachBalloonDelegate))]
	ATTACH_BALLOON,
    
    [EventHandlerAttribute()]
	DETACH_BALLOON,

    [EventHandlerAttribute(typeof(BalloonDelegate))]
	TRIGGER_BALLOON,

    [EventHandlerAttribute(typeof(Vector2Delegate))]
    STUN_BALLOON_POP
}

public enum EGameTrigger
{
    [EventHandlerAttribute()]
	LEVEL_END_REACHED
}

//    EventService.Register<>
