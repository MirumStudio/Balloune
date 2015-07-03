using UnityEngine;
using System.Collections;
using Radix.Event;

public class BalloonSensor : CharacterSensor {

	private const string IS_PULlING_PARAMTER = "IsPulling";
	private const string SPEED_PARAMTER = "Speed";

	private CharacterPull mCurrentPull = null;

	protected override void Start ()
	{
		base.Start ();

		EventService.Register<CharacterPullDelegate>(EGameEvent.BEGIN_PULLING, OnBeginPulling);
		EventService.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

	// Update is called once per frame
	void Update () {
		if (mCurrentPull != null) {
			UpdateSpeedParamater(mCurrentPull.GetPullStrength());
		} else {
			UpdateSpeedParamater(0f);
		}
	}

	public void OnBeginPulling(CharacterPull pArg)
	{
		mCurrentPull = pArg;
	}
	
	public void OnStopPulling()
	{
		mCurrentPull = null;
	}

	private void UpdateIsPullingParamater()
	{
		mAnimator.SetBool (IS_PULlING_PARAMTER, mCurrentPull != null);
	}

	private void UpdateSpeedParamater(float pSpeed)
	{
		mAnimator.SetFloat (SPEED_PARAMTER, pSpeed);
	}
}
