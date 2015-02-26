using UnityEngine;

public class StartScreenView : BaseView
{
    protected void Start()
    {
        Application.LoadLevel(SplashScreenView.SCENE_NAME);
    }
}
