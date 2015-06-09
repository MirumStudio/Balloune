/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class SplashScreenView : BaseView {

    static public new string SCENE_NAME = "SplashScreen";

    private const float DURATION = 5;
    private float mCurrentTime = 0;

    void Update()
    {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > DURATION)
        {
            Application.LoadLevel(MainMenuView.SCENE_NAME);
        }
    }

}
