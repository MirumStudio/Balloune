/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

public class LifeBehavior : BalloonBehavior
{
    private BalloonPhysics mPhysics;

	protected override void Start () {
		base.Start ();
		mPhysics = mBalloon.Physics;
	}

	void Update () {
	    
	}

    public override void OnPop()
	{
    }

	//Je crois qu'on avait dit que ce serait la scène qui ferait ce check là
    private void CheckIfGameOver()
    {
       /* if (mBalloonHolder.GetLifeBalloonCount() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }*/
    }
}
