using UnityEngine;
using System.Collections;
using System;

public delegate void VoidHandler();
public class GreyScaler : MonoBehaviour
{
    private const string SHADER_MEMBER_NAME = "_GrayScl";
    private const float MAX_GRAY_VALUE = 1f;

    public event VoidHandler OnMaxColor;

    [SerializeField]
    private float m_InitialGrayScale = 0.22f;

    [SerializeField]
    private float m_AnimationSpeed = 0.01f;

    protected float mCurrentValue = 0f;
    protected float mWantedValue = 0f;

    void Start()
    {
        SetShaderGrayScale(m_InitialGrayScale);
        mCurrentValue = m_InitialGrayScale;
        mWantedValue = m_InitialGrayScale;
    }

    void Update()
    {
        if(mCurrentValue < mWantedValue)
        {
            mCurrentValue += m_AnimationSpeed;
            SetShaderGrayScale(mCurrentValue);

            CheckForMaxValue();
        }
    }

    public void AddGreyScale(float pValue)
    {
        mWantedValue += pValue;
    }

    private void CheckForMaxValue()
    {
        if (mCurrentValue >= MAX_GRAY_VALUE && OnMaxColor != null)
        {
            OnMaxColor();
        }
    }

    private void SetShaderGrayScale(float pNewValue)
    {
        renderer.material.SetFloat(SHADER_MEMBER_NAME, Math.Min(MAX_GRAY_VALUE, pNewValue));
    }
}
