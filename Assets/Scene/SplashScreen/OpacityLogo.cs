using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpacityLogo : MonoBehaviour {

    private const float ANIMATION_DURATION_SEC = 2f;
    private const float BEGIN_WAITING_TIME = 0.5f;
    private const float FULL_OPACITY_TIME = 1f;

    private Image mLogo;
    private float mCurrentAlpha = 0f;
    private float mCurrentTime = 0f;

	void Start () {
        mLogo = GetComponent<Image>();

        if (mLogo == null)
        {
            //ERROR
        }

        UpdateLogoColor();
	}
	
	void Update () {

        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > BEGIN_WAITING_TIME)
        {
            if (mCurrentTime <= ANIMATION_DURATION_SEC + BEGIN_WAITING_TIME)
            {
                FadeIn();
            }
            else if (mCurrentTime > ANIMATION_DURATION_SEC + BEGIN_WAITING_TIME + FULL_OPACITY_TIME 
                && mCurrentTime <= 2 * ANIMATION_DURATION_SEC + FULL_OPACITY_TIME + BEGIN_WAITING_TIME)
            {
                FadeOut();
            }
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
