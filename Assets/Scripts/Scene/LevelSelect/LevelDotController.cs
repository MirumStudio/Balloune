/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Radix.Event;

public class LevelDotController : MonoBehaviour {

    private List<LevelDot> mDots;

	void Start () {
        mDots = new List<LevelDot>();
        GetDots();
        EventService.Register<LevelLoadedDelegate>(ELevelSelectEvent.LEVEL_LOADED, OnLevelInfoLoaded);
        EventService.Register<IntDelegate>(ELevelSelectEvent.LEVEL_CHANGED, OnLevelChanged);
    }
	
    private void GetDots()
    {
        LevelDot[] dots = this.GetComponentsInChildren<LevelDot>();
        foreach (LevelDot dot in dots)
        {
            mDots.Add(dot);
        }
    }

    private void OnLevelInfoLoaded(List<UILevelInfo> pLevels)
    {
        for (int i = 0; i < pLevels.Count; i++)
        {
            mDots[i].SetLevelInfo(pLevels[i]);
        }
    }

    private void OnLevelChanged(int pNewLevelIndex)
    {
        for (int i = 0; i < mDots.Count; i++)
        {
            mDots[i].Unselect();
        }

        mDots [pNewLevelIndex].Select();
    }
}
