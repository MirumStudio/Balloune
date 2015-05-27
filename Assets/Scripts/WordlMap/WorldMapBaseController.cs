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
		//TODO: Remove when other level will be created Maybe, we can create a fallback for demo purpose
        Application.LoadLevel("Level" + m_Id + "_" + (GetComponentInChildren<WordlMapCharacter>().CurrentLevel+1));
        //Application.LoadLevel("Level" + m_Id + "_" + 1);
    }

    void OnDestroy()
    {
        EventService.UnregisterAllEventListener(typeof(EWorldMapEvent));
    }
}
