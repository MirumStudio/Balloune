using UnityEngine;
using System.Collections;

public class GreyScaleTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        renderer.material.SetFloat("_GrayScl", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
