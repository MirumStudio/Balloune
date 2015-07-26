/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using Radix.Event;
using System.Collections.Generic;

public delegate void LevelLoadedDelegate(List<UILevelInfo> pLevels);
public enum ELevelSelectEvent
{
    [EventHandlerAttribute(typeof(LevelLoadedDelegate))]
    LEVEL_LOADED,

    [EventHandlerAttribute(typeof(IntDelegate))]
    LEVEL_CHANGED,

    [EventHandlerAttribute(typeof(IntDelegate))]
    WANT_CHANGE_LEVEL
}
