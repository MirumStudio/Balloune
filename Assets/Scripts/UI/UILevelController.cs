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
        EventService.Register<PopupDelegate>(EGameEvent.POPUP_DISPLAYED, OnPopupDisplayed);
        EventService.Register<PopupDelegate>(EGameEvent.POPUP_HIDED, OnPopupHided);
	}

    private void OnPopupDisplayed(UIPopupBase pArg)
    {
        gameObject.SetActive(false);
    }

    private void OnPopupHided(UIPopupBase pArg)
    {
        gameObject.SetActive(true);
    }

    public void OnPauseClick()
    {
        EventService.DispatchEvent(EGameEvent.DISPLAY_PAUSE_POPUP);
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EGameEvent));
        EventService.UnregisterAllEventListener(typeof(ETouchEvent));
        EventService.UnregisterAllEventListener(typeof(EGameTrigger));
    }
}
