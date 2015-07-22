using UnityEngine;
using System.Collections;

public class ChapterSelectController : MonoBehaviour {

    public void OnChapterClick()
    {
        //TODO : Go to load game screen
        Application.LoadLevel("LevelSelect");
    }

    public void OnBackClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }
}
