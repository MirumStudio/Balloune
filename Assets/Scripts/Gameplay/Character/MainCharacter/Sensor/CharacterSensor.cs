using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator))]
public class CharacterSensor : MonoBehaviour {

	protected Animator mAnimator;

	// Use this for initialization
	virtual protected void Start () {
		mAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
