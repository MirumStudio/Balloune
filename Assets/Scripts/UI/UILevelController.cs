/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using UnityEngine;

public class UILevelController : MonoBehaviour {

	void Start () {
        EventListener.Register(EGameEvent.POPUP_DISPLAYED, OnPopupDisplayed);
        EventListener.Register(EGameEvent.POPUP_HIDED, OnPopupHided);
	}

    private void OnPopupDisplayed(Enum pEvent, System.Object pArg)
    {
        gameObject.SetActive(false);
    }

    private void OnPopupHided(Enum pEvent, System.Object pArg)
    {
        gameObject.SetActive(true);
    }

    public void OnPauseClick()
    {
        EventService.DispatchEvent(EGameEvent.DISPLAY_PAUSE_POPUP, null);
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EGameEvent));
        EventService.UnregisterAllEventListener(typeof(EGameControl));
        EventService.UnregisterAllEventListener(typeof(ETouchEvent));
    }
}
