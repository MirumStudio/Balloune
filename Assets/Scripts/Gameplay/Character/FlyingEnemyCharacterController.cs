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

	[SerializeField]
	private float m_PatrolledDistance = 0f;
	[SerializeField]
	private float m_VerticalPatrolledDistance = 0f;

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
		m_PatrolledDistance = m_PatrolledDistance + horizontalAxisValue;
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
		m_VerticalPatrolledDistance = m_VerticalPatrolledDistance + verticalAxisValue;
		return verticalAxisValue;
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		mAnimator.UpdateAnimation(pDirection,pIsGrounded);
	}

	protected override bool shouldTurnAround()
	{
		bool shouldTurnAround = false;
		if (Mathf.Abs(m_PatrolledDistance) >= Mathf.Abs (m_PatrolRange)) 
		{
			shouldTurnAround = true;
			m_PatrolledDistance = 0;
			m_PatrolRange = m_PatrolRange * -1;
		} 
		return shouldTurnAround;
	}

	private bool shouldTurnAroundVertically()
	{
		bool shouldTurnAround = false;
		if (Mathf.Abs(m_VerticalPatrolledDistance) >= Mathf.Abs (m_VerticalPatrolRange)) 
		{
			shouldTurnAround = true;
			m_VerticalPatrolledDistance = 0;
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

