using UnityEngine;
using System.Collections;

public class SplashScreenView : BaseView {

    static public new string SCENE_NAME = "SplashScreen";

    private const float DURATION = 5;
    private float mCurrentTime = 0;

    void Update()
    {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > DURATION)
        {
            Application.LoadLevel("MainMenuView");
        }
    }

}
