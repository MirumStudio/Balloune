using UnityEngine;
using System.Collections;

public class SplashScreenView : BaseView {

    private float duration = 6;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            Application.LoadLevel("MainMenuView");
        }
	}
}
