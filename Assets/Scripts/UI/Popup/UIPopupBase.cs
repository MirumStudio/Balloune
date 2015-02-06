using UnityEngine;
using System.Collections;
using Radix.Event;

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
        EventService.DipatchEvent(EGameEvent.POPUP_DISPLAYED, this);
    }

    protected void HidePopup()
    {
        gameObject.SetActive(false);
        mPopupIsDisplay = false;
        EventService.DipatchEvent(EGameEvent.POPUP_HIDED, this);
    }

    public void OnLevelSelectClick()
    {
        Application.LoadLevel(WorldMapBaseView.SCENE_NAME + 1);
    }

    public void OnRestartClick()
    {
        Application.LoadLevel("Level1_1");
    }

    public void OnMainMenuClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }
}
