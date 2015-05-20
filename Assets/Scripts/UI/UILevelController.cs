using UnityEngine;
using System.Collections;
using System;
using Radix.Event;

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
    }
}
