/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public delegate void PopupDelegate(UIPopupBase pPopup);

//TODO: implement better popup system with priorities
public class UIPopupBase : MonoBehaviour {

    static private bool mPopupIsDisplay = false;

    static public bool PopupIsDisplayed
    {
        get { return mPopupIsDisplay; }
    }

	virtual protected void Start () {
        gameObject.SetActive(false);
	}

    private void OnDestroy()
    {
        mPopupIsDisplay = false;
    }

    protected void DisplayPopup()
    {
        gameObject.SetActive(true);
        mPopupIsDisplay = true;
        EventService.DispatchEvent(EGameEvent.POPUP_DISPLAYED, this);
    }

    protected void HidePopup()
    {
        gameObject.SetActive(false);
        mPopupIsDisplay = false;
        EventService.DispatchEvent(EGameEvent.POPUP_HIDED, this);
    }

    public void OnLevelSelectClick()
    {
        Debug.Log("ddd");
        Application.LoadLevel("LevelSelect");
    }

    public void OnRestartClick()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void OnMainMenuClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }
}
