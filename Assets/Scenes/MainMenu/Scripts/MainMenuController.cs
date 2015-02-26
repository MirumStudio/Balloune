using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayClick()
    {
        //TODO : Go to load game screen
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
