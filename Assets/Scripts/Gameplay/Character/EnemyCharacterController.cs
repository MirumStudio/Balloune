using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

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

	protected override void move(Direction pDirection)
	{
		UpdateAnimation(pDirection, mIsGrounded);
		
		Vector2 newPosition = mRigidbody2D.position;

		if (pDirection.Value != 0 && CanMove(pDirection) && !HorizontalMaxSpeedReached(pDirection))
		{
			newPosition.x = newPosition.x + (pDirection.Value *  moveSpeed * Time.deltaTime);
		}
		Debug.Log ("Before : " + newPosition.y);
		mRigidbody2D.MovePosition (newPosition);
		AjustVelocity();
		CheckFlipping(pDirection);
		UpdateJumping();
	}

	protected override int GetHorizontalAxisValue() 
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

	protected virtual bool shouldTurnAround()
	{
		return false;
	}
}

