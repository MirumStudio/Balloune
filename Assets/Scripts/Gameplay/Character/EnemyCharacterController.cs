/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

[RequireComponent (typeof(CharacterAnimator))]
public class EnemyCharacterController : BaseCharacterController
{
	[SerializeField]
	protected float moveSpeed = 1f;

	protected CharacterAnimator mAnimator;

	public override void Start ()
	{
		base.Start ();
		mAnimator=GetComponent<CharacterAnimator>();
	}
	
	protected override void Update (){
		base.Update ();
	}
	
	protected override void FixedUpdate() 
	{
		base.FixedUpdate ();
	}

	protected override void Move(float pSpeed)
	{
		Direction direction = GetDirection (pSpeed);
		UpdateAnimation(direction, mIsGrounded);
		
		Vector2 newPosition = mRigidbody2D.position;

		if (direction.Value != 0 && CanMove(direction) && !HorizontalMaxSpeedReached(direction))
		{
			newPosition.x = newPosition.x + (direction.Value *  moveSpeed * Time.deltaTime);
		}
		mRigidbody2D.MovePosition (newPosition);
		AjustVelocity();
		CheckFlipping(direction);
		UpdateJumping();
	}

	protected override float GetHorizontalAxisValue() 
	{
		return 0;
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		this.mAnimator.UpdateAnimation(pDirection,pIsGrounded);
	}
	
	protected override bool CharacterWantToJump 
	{
		get { return false;}
	}

	protected virtual bool ShouldTurnAround()
	{
		return false;
	}
}

