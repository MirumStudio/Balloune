using UnityEngine;
using System.Collections;

public class StartScreenView : BaseView
{

    protected void Start()
    {
        Application.LoadLevel(SplashScreenView.SCENE_NAME);
    }
}
