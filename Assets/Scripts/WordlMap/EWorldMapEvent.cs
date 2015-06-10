/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using Radix.Event;

public delegate void IntDelegate(int pArg);
public delegate void StringDelegate(string pArg);
public delegate void BoolDelegate(bool pArg);
public delegate void FloatDelegate(float pArg);
public delegate void Vector2Delegate(Vector2 pVec);

public delegate void EndChangeLevelHandler(LevelPoint pLevelPoint);

public enum EWorldMapEvent {
    [EventHandlerAttribute(typeof(IntDelegate))]
    WANT_CHANGE_LEVEL,

    [EventHandlerAttribute(typeof(IntDelegate))]
	BEGIN_CHANGE_LEVEL,

    [EventHandlerAttribute(typeof(EndChangeLevelHandler))]
    END_CHANGE_LEVEL
}
