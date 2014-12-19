using UnityEngine;
using System.Collections;

public class SplashScreenView : BaseView {

    private const float DURATION = 5;
    private float mCurrentTime = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > DURATION)
        {
            Application.LoadLevel("MainMenuView");
        }
	}
}
