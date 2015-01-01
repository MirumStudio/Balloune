using UnityEngine;
using System.Collections;
using System;

public delegate void VoidHandler();
public class GreyScaleTestScript : MonoBehaviour {

    public event VoidHandler OnMaxColor;

    private float currentBalue = 0.22f;

	// Use this for initialization
	void Start () {
        renderer.material.SetFloat("_GrayScl", 0.22f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddGreyScale(float pValue)
    {
        currentBalue += pValue;
        renderer.material.SetFloat("_GrayScl", Math.Min(1f, currentBalue));

        if(currentBalue >= 1f && OnMaxColor != null)
        {
            OnMaxColor();
        }
    }
}
