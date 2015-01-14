using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;
using System;

public class LevelController : BaseView {

    public EGameEvent allo;

    public GameObject but;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        GetComponentInChildren<MainCharacterController>().OnKidHit += OnKidHit;
        GetComponentInChildren<GreyScaler>().OnMaxColor += OnFinish;
        but.SetActive(false);
        EventListener.Register(EGameEvent.TEST, Test);
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

    private void Test(Enum lol, System.Object arg)
    {
        Debug.Log("EVENT !!!!!");
    }
}
