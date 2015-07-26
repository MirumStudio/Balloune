/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Radix.Event;

public class LevelDot : MonoBehaviour {

    private Image mImage;
    private LevelDotLock mLock;
    private UILevelInfo mUILevelInfo;

    private bool mIsSelected = false;

    private void Start()
    {
        mImage = GetComponent<Image>();
        mLock = GetComponentInChildren<LevelDotLock>();
    }

	public void SetLevelInfo(UILevelInfo pInfo)
    {
        mUILevelInfo = pInfo;
        mLock.UpdateLockVisibility(!mUILevelInfo.IsUnlocked);
        UpdateColor();
    }

    public void Select()
    {
        mIsSelected = true;
        UpdateColor();
    }

    public void Unselect()
    {
        mIsSelected = false;
        UpdateColor();

    }

    private void UpdateColor()
    {
        if (mIsSelected)
        {
            mImage.color = SelectedColor;
        }
        else if (mUILevelInfo.IsUnlocked)
        {
            mImage.color = UnlockedColor;
        } else
        {
            mImage.color = LockedColor;
        }
    }

    public void OnClick()
    {
        EventService.DispatchEvent(ELevelSelectEvent.WANT_CHANGE_LEVEL, mUILevelInfo.Index - 1);
    }

    private Color32 SelectedColor
    {
        get { return new Color32(183, 55, 9, 255); }
    }

    private Color32 UnlockedColor
    {
        get { return new Color32(117, 124, 127, 255); }
    }

    private Color32 LockedColor
    {
        get { return new Color32(52, 56, 58, 255); }
    }
}
