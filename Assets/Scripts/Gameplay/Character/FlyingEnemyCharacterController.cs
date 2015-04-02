using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

[RequireComponent (typeof(CharacterAnimator))]
public class FlyingEnemyCharacterController : EnemyCharacterController
{
	[SerializeField]
	private float m_PatrolRange = 100f;
	[SerializeField]
	private float m_VerticalPatrolRange = 100f;

	private float mPatrolledDistance = 0f;
	private float mVerticalPatrolledDistance = 0f;

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
		var direction = new Direction(GetVerticalAxisValue());
		moveVertically (direction);
	}
	
	protected override int GetHorizontalAxisValue() 
	{
		int horizontalAxisValue = 0;
		if (m_PatrolRange > 0) 
		{
			horizontalAxisValue = 1;
		} else if (m_PatrolRange < 0) {
			horizontalAxisValue = -1;
		}
		
		bool turnAround = shouldTurnAround ();
		if (turnAround) {
			horizontalAxisValue = horizontalAxisValue * -1;
		}
		mPatrolledDistance = mPatrolledDistance + horizontalAxisValue;
		return horizontalAxisValue;
	}

	protected int GetVerticalAxisValue() 
	{
		int verticalAxisValue = 0;
		if (m_VerticalPatrolRange > 0) 
		{
			verticalAxisValue = 1;
		} else if (m_VerticalPatrolRange < 0) {
			verticalAxisValue = -1;
		}
		
		bool turnAround = shouldTurnAroundVertically ();
		if (turnAround) {
			verticalAxisValue = verticalAxisValue * -1;
		}
		mVerticalPatrolledDistance = mVerticalPatrolledDistance + verticalAxisValue;
		return verticalAxisValue;
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

	private bool shouldTurnAroundVertically()
	{
		bool shouldTurnAround = false;
		if (Mathf.Abs(mVerticalPatrolledDistance) >= Mathf.Abs (m_VerticalPatrolRange)) 
		{
			shouldTurnAround = true;
			mVerticalPatrolledDistance = 0;
			m_VerticalPatrolRange = m_VerticalPatrolRange * -1;
		} 
		return shouldTurnAround;
	}

	protected override bool CharacterWantToJump 
	{
		get { return false;}
	}

	private void moveVertically(Direction pDirection)
	{
		if (pDirection.Value != 0)
		{
			AddVerticalForce(pDirection);
		}
	}

	private void AddVerticalForce(Direction pDirection)
	{
		mRigidbody2D.AddForce(Vector2.up * pDirection.Value * moveForce);
	}
}

