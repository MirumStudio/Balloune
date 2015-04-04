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
		//base.Update ();
		var xDirection = new Direction(GetHorizontalAxisValue());
		var yDirection = new Direction(GetVerticalAxisValue());
		move (xDirection, yDirection);
	}
	
	protected override void FixedUpdate() 
	{
		base.FixedUpdate ();
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
		m_PatrolledDistance = m_PatrolledDistance + horizontalAxisValue  * moveSpeed * Time.deltaTime;
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
		m_VerticalPatrolledDistance = m_VerticalPatrolledDistance + verticalAxisValue * moveSpeed * Time.deltaTime;
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

	protected void move(Direction pXDirection, Direction pYDirection)
	{
		UpdateAnimation(pXDirection, mIsGrounded);
		
		Vector2 newPosition = mRigidbody2D.position;

		if (pXDirection.Value != 0 && CanMove(pXDirection) && !HorizontalMaxSpeedReached(pXDirection))
		{
			newPosition.x = newPosition.x + (pXDirection.Value *  moveSpeed * Time.deltaTime);
		}
		if (pYDirection.Value != 0 && CanMove (pYDirection)) 
		{
			newPosition.y = newPosition.y + (pYDirection.Value *  moveSpeed * Time.deltaTime);
		}

		mRigidbody2D.MovePosition (newPosition);
		AjustVelocity();
		CheckFlipping(pXDirection);
		UpdateJumping();
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
		//mRigidbody2D.AddForce(Vector2.up * pDirection.Value * moveForce);
		//Debug.Log ("mRigidbody2D.position.y : " + mRigidbody2D.position.y);
		//Debug.Log ("pDirection.Value : " + pDirection.Value);
		Vector2 newPosition = mRigidbody2D.position;
		newPosition.y = newPosition.y + pDirection.Value;
		mRigidbody2D.MovePosition (newPosition);
	}
}

