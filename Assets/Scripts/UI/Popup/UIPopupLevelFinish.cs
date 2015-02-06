using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class UIPopupLevelFinish : UIPopupBase {

	override protected void Start () {
        base.Start();
        EventListener.Register(EGameEvent.LEVEL_FINISHED, OnLevelFinish);
	}

    private void OnLevelFinish(Enum pEvent, System.Object pArg)
    {
        DisplayPopup();
    }

    public void OnNextLevelClick()
    {
        OnLevelSelectClick();
    }
}
