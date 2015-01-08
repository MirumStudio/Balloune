using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    public GameObject but;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<MainCharacterController>().OnKidHit += OnKidHit;
        GetComponentInChildren<GreyScaler>().OnMaxColor += OnFinish;
        but.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnKidHit()
    {
        GetComponentInChildren<GreyScaler>().AddGreyScale(0.16f);
       // but.SetActive(true);
    }

    private void OnFinish()
    {
        but.SetActive(true);
    }
}
