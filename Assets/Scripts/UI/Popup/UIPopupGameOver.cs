using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class UIPopupGameOver : UIPopupBase
{

    override protected void Start()
    {
        base.Start();
        EventListener.Register(EGameEvent.GAME_OVER, OnLevelFinish);
    }

    private void OnLevelFinish(Enum pEvent, System.Object pArg)
    {
        DisplayPopup();
    }
}
