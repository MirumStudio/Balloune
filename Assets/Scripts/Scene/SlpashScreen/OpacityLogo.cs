/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using UnityEngine.UI;

public class OpacityLogo : MonoBehaviour {

    private const float ANIMATION_DURATION_SEC = 2f;
    private const float FULL_OPACITY_TIME = 1f;

    private Image mLogo;
    private float mCurrentAlpha = 0f;
    private float mCurrentTime = 0f;

	void Start () {
        mLogo = GetComponent<Image>();
        UpdateLogoColor();
	}
	
	void Update () {

        mCurrentTime += Time.deltaTime;

        if (mCurrentTime <= ANIMATION_DURATION_SEC)
        {
            FadeIn();
        }
        else if (mCurrentTime > ANIMATION_DURATION_SEC + FULL_OPACITY_TIME 
            && mCurrentTime <= 2 * ANIMATION_DURATION_SEC + FULL_OPACITY_TIME)
        {
            FadeOut();
        }

        UpdateLogoColor();
	}

    private void FadeIn()
    {
        mCurrentAlpha += Time.deltaTime / ANIMATION_DURATION_SEC;
    }

    private void FadeOut()
    {
        mCurrentAlpha -= Time.deltaTime / ANIMATION_DURATION_SEC;
    }

    private void UpdateLogoColor()
    {
        mLogo.color = new Color(1f, 1f, 1f, mCurrentAlpha);
    }
}
