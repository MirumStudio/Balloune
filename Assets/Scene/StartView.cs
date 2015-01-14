using UnityEngine;
using System.Collections;
using Radix.Service;

public class StartView : BaseView {

	// Use this for initialization
	override protected void Start () {
        Application.LoadLevel("SplashScreenView");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
