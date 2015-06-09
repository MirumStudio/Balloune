/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

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
		SetAnimationDirection(ANIMATION_RIGHT_DIRECTION);
	}
	
	public void UpdateAnimation(Direction pDirection, bool pIsGrounded, bool pIsRunning)
	{
		if (pDirection.Edge == EEdge.NONE)
		{
			StopAnimation();
		}
		else
		{
			UpdateAnimationDirection(pDirection, pIsGrounded);
            UpdateAnimationSpeed(pIsRunning);
		}
	}
	
	public void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		UpdateAnimation (pDirection, pIsGrounded, false);
	}
	
	private void StopAnimation()
	{
        if (mAnimator == null) return;
		mAnimator.speed = 0;
	}
	
	#region AnimationDirection
	private void UpdateAnimationDirection(Direction pDirection, bool pIsGrounded)
	{
		//Instead, we use flipping in BaseCharacterController
		//This function is keep for reference
		return;
		/*if (pIsGrounded)
        {
            if (pDirection.IsLeftDirection())
            {
                SetAnimationDirection(ANIMATION_LEFT_DIRECTION);
            }
            else if (pDirection.IsRightDirection())
            {
                SetAnimationDirection(ANIMATION_RIGHT_DIRECTION);
            }
        }
        else
        {
            //JUMP ANIMATION
        }*/
	}
	
	private void SetAnimationDirection(int pAnimationDirection)
	{
		mAnimator.SetInteger(ANIMATION_PARAM_DIRECTION_NAME, pAnimationDirection);
	}
	#endregion
	
	#region AnimationSpeed
	private void UpdateAnimationSpeed(bool pIsRunning)
	{
        if (mAnimator == null) return;
		float speed = 0;
		if (pIsRunning)
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
