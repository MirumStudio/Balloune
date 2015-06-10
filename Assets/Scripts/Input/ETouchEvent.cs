/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;

public delegate void SwipeBeginHandler(ESwipeDirection pDirection);

public enum ETouchEvent
{
    [EventHandlerAttribute(typeof(SwipeBeginHandler))]
    SWIPE_BEGIN,

    [EventHandlerAttribute(typeof(FloatDelegate))]
    SWIPE_END,

    [EventHandlerAttribute(typeof(Vector2Delegate))]
    TAP,

    [EventHandlerAttribute(typeof(Vector2Delegate))]
    DOUBLE_TAP,

    [EventHandlerAttribute()]
    CANCEL,

    [EventHandlerAttribute()]
    END,

    [EventHandlerAttribute()]
    MOVE
}
