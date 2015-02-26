using Radix.Event;
using UnityEngine;

public class WorldMapBaseController : MonoBehaviour {

    [SerializeField]
    private int m_Id = 1;

    public void OnExitClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }

    public void OnPlayClick()
    {
        Application.LoadLevel("Level" + m_Id + "_" + (GetComponentInChildren<WordlMapCharacter>().CurrentLevel+1));
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EWorldMapEvent));
    }
}
