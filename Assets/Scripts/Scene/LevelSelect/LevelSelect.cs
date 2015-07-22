using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    public void OnBackClick()
    {
        Application.LoadLevel("ChapterSelect");
    }
}
