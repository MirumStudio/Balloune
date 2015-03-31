using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

[RequireComponent (typeof(CharacterAnimator))]
public class EnemyCharacterController : BaseCharacterController
{
	private CharacterAnimator mAnimator;
	
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
	
	protected override int GetHorizontalAxisValue() 
	{
		float horizontalAxisValue = 0;
		bool turnAround = shouldTurnAround ();
		if (turnAround) {
			m_IsFacingRight = !m_IsFacingRight;
		}
		if (m_IsFacingRight) 
		{
			horizontalAxisValue = 1;
		} else if (!m_IsFacingRight) {
			horizontalAxisValue = -1;
		}
		else {
			Error.Create("Enemy does not know where to go", EErrorSeverity.MINOR);
		}
		//Debug.Log ("m_IsFacingRight : " + m_IsFacingRight + " turnAround : " + turnAround);
		
		return (int)Mathf.Sign(horizontalAxisValue);
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		this.mAnimator.UpdateAnimation(pDirection,pIsGrounded);
	}
	
	protected override bool CharacterWantToJump 
	{
		get { return false;}
	}

	protected override void AddForce(Direction pDirection)
	{
		mRigidbody2D.AddForce(Vector2.right * pDirection.Value * moveForce);
	}

	private bool shouldTurnAround(){
		bool shouldTurnAround = false;
		if (m_IsFacingRight && mEdgeChecker.TouchSomething (EEdge.RIGHT)) 
		{
			shouldTurnAround = true;
		} else if (!m_IsFacingRight && mEdgeChecker.TouchSomething (EEdge.LEFT)) 
		{
			shouldTurnAround = true;
		}

		return shouldTurnAround;
	}
}

