using UnityEngine;
using System.Collections;

public class MainMenuController : BaseController {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayClick()
    {
        Application.LoadLevel("WorldMapView");
    }

    public void OnLoadClick()
    {
        Application.LoadLevel("LoadScreenView");
    }

    public void OnOptionClick()
    {
        Application.LoadLevel("OptionMenuView");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
