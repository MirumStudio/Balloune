using UnityEngine;
using System.Collections;
using Radix.Event;
using Radix.Error;

[RequireComponent (typeof(CharacterAnimator))]
public class WalkingEnemyCharacterController : EnemyCharacterController
{
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
		int horizontalAxisValue = 0;
		if (m_IsFacingRight) 
		{
			horizontalAxisValue = 1;
		} else if (!m_IsFacingRight) {
			horizontalAxisValue = -1;
		}
		else {
			Error.Create("Enemy does not know where to go", EErrorSeverity.MINOR);
		}
		
		bool turnAround = shouldTurnAround ();
		if (turnAround) {
			horizontalAxisValue = horizontalAxisValue * -1;
		}
		
		return horizontalAxisValue;
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		this.mAnimator.UpdateAnimation(pDirection,pIsGrounded);
	}
	
	protected override bool CharacterWantToJump 
	{
		get { return false;}
	}
	
	protected override bool shouldTurnAround(){
		bool shouldTurnAround = false;
		if (isAboutToFall()) 
		{
			shouldTurnAround = true;
		} else if (m_IsFacingRight && mEdgeChecker.TouchSomething (EEdge.RIGHT)) 
		{
			shouldTurnAround = true;
		} else if (!m_IsFacingRight && mEdgeChecker.TouchSomething (EEdge.LEFT)) 
		{
			shouldTurnAround = true;
		}
		
		return shouldTurnAround;
	}
	
	private bool isAboutToFall(){
		bool isAboutToFall = false;
		
		if (m_IsFacingRight) 
		{
			isAboutToFall = mEdgeChecker.isOnEdgeOfPlatform (EEdge.RIGHT);
		} else 
		{
			isAboutToFall = mEdgeChecker.isOnEdgeOfPlatform (EEdge.LEFT);
		}
		return isAboutToFall;
	}
}

