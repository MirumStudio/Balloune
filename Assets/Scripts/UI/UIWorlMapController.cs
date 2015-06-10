/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.ErrorMangement;
using Radix.Event;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIWorlMapController : MonoBehaviour {

    public void Start()
    {
        EventService.Register<IntDelegate>(EWorldMapEvent.BEGIN_CHANGE_LEVEL, OnBeginChangeLevel);
        EventService.Register<EndChangeLevelHandler>(EWorldMapEvent.END_CHANGE_LEVEL, OnEndChangeLevel);
    }

    public void FadeOut()
    {
        //TODO: Real Fade Out
        GetComponentInParent<CanvasGroup>().alpha = 0f;
    }

    public void FadeIn()
    {
        //TODO: Real Fade In
        GetComponentInParent<CanvasGroup>().alpha = 1f;
    }

    public void ChangeLevel(LevelPoint pLevel)
    {
        var text = transform.FindChild("Title").GetComponent<Text>();
        text.text = pLevel.LevelName;
        FadeIn();
    }

    public void OnBeginChangeLevel(int pWantedLevel)
    {
        FadeOut();
    }

    public void OnEndChangeLevel(LevelPoint pLevel)
    {
        ChangeLevel(pLevel);
    }
}
