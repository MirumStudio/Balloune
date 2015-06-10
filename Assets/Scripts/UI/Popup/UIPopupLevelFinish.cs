/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;

public class UIPopupLevelFinish : UIPopupBase {

	override protected void Start () {
        base.Start();
        EventService.Register(EGameEvent.LEVEL_FINISHED, OnLevelFinish);
	}

    private void OnLevelFinish()
    {
        DisplayPopup();
    }

    public void OnNextLevelClick()
    {
        OnLevelSelectClick();
    }
}
