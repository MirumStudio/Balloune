﻿/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class BalloonControl : MonoBehaviour {
	private const string BALLOON_IDENTIFIER = "Balloon";
	
	private GameObject mTouchedBalloonObject = null;
	private Balloon mTouchedBalloon = null;
	private BalloonPhysics mTouchedBalloonPhysics = null;
	
	private static bool mIsJumpCommand = false;	
	
	void Start () {
		EventService.Register<Vector2Delegate>(ETouchEvent.TAP, OnTap);
		EventService.Register<Vector2Delegate>(ETouchEvent.DOUBLE_TAP, OnDoubleTap);
		EventService.Register(ETouchEvent.END, OnEndTouch);
		EventService.Register<SwipeBeginHandler>(ETouchEvent.SWIPE_BEGIN, OnSwipeBegin);
		EventService.Register<FloatDelegate>(ETouchEvent.SWIPE_END, OnSwipeEnd);
	}
	
	
	private void OnTap(Vector2 pPosition)
	{
		var touchedBalloon = GetTouchedBalloon(pPosition);
		
		if(touchedBalloon != null)
		{
			mTouchedBalloonObject = touchedBalloon;
			PickupBalloon();
		}
	}
	
	private void OnDoubleTap(Vector2 pPosition)
	{
		var touchedBalloon = GetTouchedBalloon(pPosition);
		
		if (touchedBalloon != null)
		{
			EventService.DispatchEvent(EGameEvent.TRIGGER_BALLOON, touchedBalloon.GetComponent<Balloon>());
		}
		OnTap (pPosition);
	}
	
	private void OnEndTouch()
	{
		if (mTouchedBalloonObject != null)
		{
			DropBalloon();
		}
	}
	
	private void OnSwipeBegin(ESwipeDirection pDirection)
	{
		if (mTouchedBalloonObject != null && pDirection == ESwipeDirection.UP/* && mTouchedBalloon.Type == EBalloonType.LIFE*/
		    && TouchService.CurrentTouchPosition.y > mTouchedBalloonObject.transform.position.y)
		{
			mIsJumpCommand = true;
		}
	}
	
	private void OnSwipeEnd(float pDistance)
	{
		if(mTouchedBalloonObject != null)
		{
			mIsJumpCommand = false;
		}
	}
	
	private GameObject GetTouchedBalloon(Vector3 pWorldPosition)
	{
		Vector2 worldPosition2D = new Vector2(pWorldPosition.x, pWorldPosition.y);
		Collider2D[] touchedColliders = Physics2D.OverlapCircleAll (worldPosition2D,  0.5f);
		if (touchedColliders != null && touchedColliders.Length > 0) {
			float minDistance = float.MaxValue;
			int minDistanceBalloonIndex = 0;
			for (int i = 0; i < touchedColliders.Length; i++) {
				if (touchedColliders[i].name.Contains (BALLOON_IDENTIFIER)) {
					float distance = Vector2.Distance (touchedColliders [i].gameObject.transform.position, pWorldPosition);
					if (distance < minDistance) {
						minDistance = distance;
						minDistanceBalloonIndex = i;
						mTouchedBalloonObject = touchedColliders [minDistanceBalloonIndex].gameObject;
					}
				}
			}
		} else {
			mTouchedBalloonObject = null;
		}


		return mTouchedBalloonObject;
	}
	
	private void PickupBalloon()
	{
		if (mTouchedBalloon == null)
		{
			mTouchedBalloon = mTouchedBalloonObject.GetComponent<Balloon>();
			mTouchedBalloonPhysics = mTouchedBalloon.Physics;
			EventService.DispatchEvent(EGameEvent.PICKUP_BALLOON, mTouchedBalloon);
		}
	}
	
	private void DropBalloon()
	{
		if (mTouchedBalloonObject != null && mTouchedBalloon != null)
		{
			EventService.DispatchEvent(EGameEvent.DROP_BALLOON, mTouchedBalloon);
			mTouchedBalloonObject = null;
			mTouchedBalloonPhysics = null;
			mTouchedBalloon = null;
			mIsJumpCommand = false;
		}
	}
	
	public static bool IsJumpCommand()
	{
		bool value = mIsJumpCommand;
		mIsJumpCommand = false;
		return value;
	}
}