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
        if (!mIsRed && mAnimator.GetBool(FURY_PARAMATER))
        {
            mRenderer.color = Color.red;
        } else if(mIsRed)
        {
            mRenderer.color = Color.white;
        }
    }
}
