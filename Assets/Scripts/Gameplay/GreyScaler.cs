/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using System;
using UnityEngine;

public class GreyScaler : MonoBehaviour
{
    private const string SHADER_MEMBER_NAME = "_GrayScl";
    private const float MAX_GRAY_VALUE = 1f;

    [SerializeField]
    private float m_InitialGrayScale = 0.22f;

    [SerializeField]
    private float m_AnimationSpeed = 0.01f;

    protected float mCurrentValue = 0f;
    protected float mWantedValue = 0f;
    protected float mStepValue;

    private Renderer mRenderer;

    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        SetShaderGrayScale(m_InitialGrayScale);
        mCurrentValue = m_InitialGrayScale;
        mWantedValue = m_InitialGrayScale;
        mStepValue = (1f - m_InitialGrayScale) / LevelInfo.ChildCount;
        EventService.Register<BalloonDelegate>(EGameEvent.BALLOON_GIVEN, OnBalloonGiven);
    }

    void Update()
    {
        if(mCurrentValue < mWantedValue)
        {
            mCurrentValue += m_AnimationSpeed;
            mCurrentValue = Math.Min(mCurrentValue, MAX_GRAY_VALUE);
            SetShaderGrayScale(mCurrentValue);
        }
    }

    private void AddGreyScale(float pValue)
    {
        mWantedValue += pValue;
    }

    private void SetShaderGrayScale(float pNewValue)
    {
        mRenderer.material.SetFloat(SHADER_MEMBER_NAME, Math.Min(MAX_GRAY_VALUE, pNewValue));
    }

    private void OnBalloonGiven(Balloon pBalloon)
    {
        AddGreyScale(mStepValue);
    }
}
