using UnityEngine;
using System.Collections;
using Radix.Event;

public class PlateformSensor : CharacterSensor {

	private float AJUST_X = 0.5f;

	Balloon mBalloon = null;

	protected override void Start ()
	{
		base.Start ();
		
		EventService.Register<CharacterPullDelegate>(EGameEvent.BEGIN_PULLING, OnBeginPulling);
		EventService.Register(EGameEvent.END_PULLING, OnStopPulling);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName("Moving"))
		{
			float speed = mAnimator.GetFloat(SPEED_PARAMTER);
			
			Vector2 bottom;
			Vector2 top;
			
			if(speed < 0)
			{
				//CheckLeft();
				bottom = GetBottomLeftCorner();
				top = GetTopLeftCorner();
				bottom.x -= AJUST_X;
				top.x -= AJUST_X;
				
				Check(bottom, top, GetTopRightCorner());
				
			}
			else if(speed > 0)
			{
				bottom = GetBottomRightCorner();
				top = GetTopRightCorner();
				bottom.x += AJUST_X;
				top.x += AJUST_X;
				
				Check(bottom, top, GetTopLeftCorner());
			}
		}
		
	}
	
	public void OnBeginPulling(CharacterPull pArg, Balloon pBalloon)
	{
		mBalloon = pBalloon;
	}
	
	public void OnStopPulling()
	{
		mBalloon = null;
	}
	
	private void Check(Vector2 mBottom, Vector2 mTop, Vector2 pOther)
	{
		mTop.y += 0.5f;
		pOther.y += 0.5f;
		Vector2 ultraTop = mTop;
		
		ultraTop.y += 2f;
		
		mBottom.y += 0.3f;
		Debug.DrawLine (mTop, pOther, Color.yellow);
		Debug.DrawLine (mTop, mBottom, Color.green);
		if(Physics2D.Linecast(mTop, pOther, PlateformLayerMask))
		{
			CheckBalloon(mTop.y);
		}
	}

	private void CheckBalloon(float pY)
	{
		if (mBalloon.transform.position.y > pY) {
			mAnimator.SetBool ("HaveToJump", true);
		}
	}

}
