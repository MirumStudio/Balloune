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
        GetComponentInChildren<GreyScaler>().OnMaxColor += OnFinish;
        but.SetActive(false);
        EventListener.Register(EGameEvent.CHILD_COLLISION, Test);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnFinish()
    {
        but.SetActive(true);
    }

    private void Test(Enum lol, System.Object arg)
    {
        GetComponentInChildren<GreyScaler>().AddGreyScale(0.16f);
    }
}
