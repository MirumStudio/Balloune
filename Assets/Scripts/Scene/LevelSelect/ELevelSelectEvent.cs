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
    LEVEL_CHANGED
}
