using UnityEngine;
using System.Collections;

public class FuryCharacterController : MonoBehaviour {

    private const string FURY_PARAMATER = "IsInFury";

    protected Animator mAnimator;
    protected SpriteRenderer mRenderer;

    private bool mIsRed = false;

    virtual protected void Start () {
        mAnimator = GetComponent<Animator> ();
        mRenderer = GetComponent<SpriteRenderer> ();
	}
	
	void Update () {
        UpdateColor();
	}

    private void UpdateColor()
    {
        bool isInFury = mAnimator.GetBool(FURY_PARAMATER);
        if (!mIsRed && isInFury)
        {
            mRenderer.color = Color.red;
            mIsRed = true;
        } else if(mIsRed && !isInFury)
        {
            mRenderer.color = Color.white;
            mIsRed = false;
        }
    }
}
