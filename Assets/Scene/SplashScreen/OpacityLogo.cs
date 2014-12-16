using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpacityLogo : MonoBehaviour {

    private const float ANIMATION_DURATION_SEC = 2f;

    private Color COLOR_TRANSPARENT = new Color(1f, 1f, 1f, 0f);
    private Image mLogo;
    private float mCurrentAlpha = 0f;
    private float mCurrentTime = 0f;

	void Start () {
        mLogo = GetComponent<Image>();
        UpdateLogoColor();
	}
	
	void Update () {

        mCurrentTime += Time.deltaTime;
        if (mCurrentTime > 1f)
        {
            if (mCurrentTime <= ANIMATION_DURATION_SEC + 1)
            {
                mCurrentAlpha += Time.deltaTime / ANIMATION_DURATION_SEC;
            }
            else if (mCurrentTime > ANIMATION_DURATION_SEC + 2 && mCurrentTime <= 2 * ANIMATION_DURATION_SEC + 2)
            {
                mCurrentAlpha -= Time.deltaTime / ANIMATION_DURATION_SEC;
            }
        }

        UpdateLogoColor();
	}

    private void UpdateLogoColor()
    {
        mLogo.color = new Color(1f, 1f, 1f, mCurrentAlpha);
    }
}
