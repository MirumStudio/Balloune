using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class UIPopupPause : UIPopupBase
{
    override protected void Start()
    {
        base.Start();
        EventListener.Register(EGameEvent.DISPLAY_PAUSE_POPUP, OnLevelFinish);
    }

    private void OnLevelFinish(Enum pEvent, System.Object pArg)
    {
        DisplayPopup();
    }

    public void OnSettingClick()
    {
        OnMainMenuClick();
    }

    public void OnCloseClick()
    {
        HidePopup();
    }
}
