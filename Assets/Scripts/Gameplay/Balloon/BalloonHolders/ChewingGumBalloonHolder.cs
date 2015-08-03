/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class ChewingGumBalloonHolder : BalloonHolder
{
	public const string CHEWING_KID_BALLOON_HOLDER_NAME = "ChewingGumBalloonHolder";
	private const float MAX_NOT_CHEWING_TIME = 1.5f;
	
	private ChewingKid mKidOwner;

	private float mNotChewingTime = 0f;

	protected override void Start()
	{
		base.Start ();
		m_MaxBalloonCount = 1;
		mKidOwner = mOwner.GetComponent<ChewingKid>();
	}

	void Update()
	{
		UpdateBalloon ();
	}

	private void UpdateBalloon()
	{
		if (mBalloons.Count == 0) {
			CreateGumBalloon();
		} else {
			ResizeBalloon();
		}
	}

	private void CreateGumBalloon()
	{
		CreateBalloon (EBalloonType.GUM);
	}

	private void ResizeBalloon()
	{
		if (!mBalloons [0].IsDeflating () && !mBalloons [0].IsInflating ()) {
			mNotChewingTime += Time.deltaTime;
		}
		if(mNotChewingTime >= MAX_NOT_CHEWING_TIME)
		{
			//If balloon is at base scale, deflate. Else, inflate.
			if(mBalloons[0].IsFullSize())
			{
				mBalloons[0].SetDeflate(true);
			} else{
				mBalloons[0].SetInflate (true);
			}
			mNotChewingTime = 0f;
		}
	}

	protected override void AttachBalloon(Balloon pBalloon)
	{
		//Do nothing
	}
	
	public override void DetachBalloon(Balloon pBalloonToDetach)
	{
		((GumBalloon)pBalloonToDetach).SetIsAttachedToKid (false);
		pBalloonToDetach.SetInflate (true);
		base.DetachBalloon (pBalloonToDetach);
	}
}

