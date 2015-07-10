using UnityEngine;
using System.Collections;

public class GasTrail : MonoBehaviour {

	private ParticleSystem mParticleSystem;

	private float mDuration = 0f;
	private float mTimeElapsed = 0f;


	void Start () {
		mParticleSystem = GetComponent<ParticleSystem> ();
		mDuration = mParticleSystem.duration * 2; //Times two to make sure the particles have time to fade out
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfFinished ();
	}

	private void CheckIfFinished()
	{
		mTimeElapsed += Time.deltaTime;
		if (mTimeElapsed >= mDuration) {
			DestroyObject (gameObject);
		}
	}
}
