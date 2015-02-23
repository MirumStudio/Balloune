using UnityEngine;
using System.Collections;
using Radix.Event;

public class WorldMapBaseController : MonoBehaviour {

    public void OnExitClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }

    public void OnPlayClick()
    {
        Application.LoadLevel("Level1_" + (GetComponentInChildren<WordlMapCharacter>().mCurrentLevel+1));
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EWorldMapEvent));
    }
}
