using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

[RequireComponent (typeof(CharacterAnimator))]
public class FlyingEnemyCharacterController : EnemyCharacterController
{
	[SerializeField]
	private float m_PatrolRange = 100f;

	private float mPatrolledDistance = 0f;

	public override void Start ()
	{
		base.Start ();
	}
	
	protected override void Update (){
		base.Update ();
	}
	
	protected override void FixedUpdate() 
	{
		base.FixedUpdate ();
	}
	
	protected override int GetHorizontalAxisValue() 
	{
		float horizontalAxisValue = 0;
		if (m_PatrolRange > 0) 
		{
			horizontalAxisValue = 1;
		} else if (m_PatrolRange < 0) {
			horizontalAxisValue = -1;
		}
		else {
			Error.Create("Enemy does not know where to go", EErrorSeverity.MINOR);
		}
		
		bool turnAround = shouldTurnAround ();
		if (turnAround) {
			horizontalAxisValue = horizontalAxisValue * -1;
		}
		mPatrolledDistance = mPatrolledDistance + horizontalAxisValue;
		return (int)Mathf.Sign(horizontalAxisValue);
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		mAnimator.UpdateAnimation(pDirection,pIsGrounded);
	}

	protected override bool shouldTurnAround()
	{
		bool shouldTurnAround = false;
		if (Mathf.Abs(mPatrolledDistance) >= Mathf.Abs (m_PatrolRange)) 
		{
			shouldTurnAround = true;
			mPatrolledDistance = 0;
			m_PatrolRange = m_PatrolRange * -1;
		} 
		return shouldTurnAround;
	}

	protected override bool CharacterWantToJump 
	{
		get { return false;}
	}
	
	protected override void AddForce(Direction pDirection)
	{
		mRigidbody2D.AddForce(Vector2.right * pDirection.Value * moveForce);
	}
}

