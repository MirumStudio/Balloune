using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClick()
    {
        Application.LoadLevel(WorldMapBaseView.SCENE_NAME + 1);
    }

    public void OnAchievementClick()
    {
       
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
