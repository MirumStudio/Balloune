using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnbackClick()
    {
        Application.LoadLevel(MainMenuView.SCENE_NAME);
    }
}
