using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    private const string ANIMATION_PARAM_DIRECTION_NAME = "Direction";
	private const string ANIMATION_PARAM_SPEED_NAME = "Speed";
    private const int ANIMATION_LEFT_DIRECTION = 0;
    private const int ANIMATION_RIGHT_DIRECTION = 1;

    [SerializeField]
    private float m_WalkAnimationSpeed = 1f;
    [SerializeField]
    private float m_RunAnimationSpeed = 2f;

    private Animator mAnimator;

	void Start () {
        mAnimator = GetComponent<Animator>();
	}
	
	public void UpdateAnimation(float pDirection, bool pIsGrounded, bool mIsRunning)
    {
        if (pDirection == 0)
        {
            StopAnimation();
        }
        else
        {
            UpdateAnimationDirection(pDirection, pIsGrounded);

            UpdateAnimationSpeed(mIsRunning);
        }
    }

    private void StopAnimation()
    {
        mAnimator.speed = 0;
    }

    #region AnimationDirection
    private void UpdateAnimationDirection(float pDirection, bool pIsGrounded)
    {
        if (pIsGrounded)
        {
            if (IsLeftDirection(pDirection))
            {
                SetAnimationDirection(ANIMATION_LEFT_DIRECTION);
            }
            else if (IsRightDirection(pDirection))
            {
                SetAnimationDirection(ANIMATION_RIGHT_DIRECTION);
            }
        }
        else
        {
            //JUMP ANIMATION
        }
    }

    private bool IsLeftDirection(float pDirection)
    {
        return (pDirection < 0);
    }

    private bool IsRightDirection(float pDirection)
    {
        return (pDirection > 0);
    }

    private void SetAnimationDirection(int pAnimationDirection)
    {
        mAnimator.SetInteger(ANIMATION_PARAM_DIRECTION_NAME, pAnimationDirection);
    }
    #endregion

    #region AnimationSpeed
    private void UpdateAnimationSpeed(bool mIsRunning)
    {
		float speed = 0;
        if (mIsRunning)
        {
			speed=1;
            SetRunAnimationSpeed();
        }
        else
        {
            SetWalkAnimationSpeed();
        }
		mAnimator.SetFloat(ANIMATION_PARAM_SPEED_NAME,speed);
    }

    private void SetWalkAnimationSpeed()
    {
        mAnimator.speed = m_WalkAnimationSpeed;
    }

    private void SetRunAnimationSpeed()
    {
        mAnimator.speed = m_RunAnimationSpeed;
    }
#endregion
}
