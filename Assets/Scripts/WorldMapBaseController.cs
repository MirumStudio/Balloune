using UnityEngine;
using System.Collections;

public class WorldMapBaseController : MonoBehaviour {

    public void OnExitClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }

    public void OnPlayClick()
    {
        Application.LoadLevel("Level1_1");
    }
}
