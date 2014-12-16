using UnityEngine;
using System.Collections;

public class MainMenuViewOld : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPlayButtonClick()
    {
        Application.LoadLevel("GameplayVincent");
    }

	public void OnPlayButtonLeandreClick()
	{
		Application.LoadLevel("GameplayLeandre");
	}
}
