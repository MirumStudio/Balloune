using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class BaseCharacterController : MonoBehaviour 
{
	[SerializeField]
	protected float m_MoveForce = 350f;
	[SerializeField]
	private float m_MaxSpeed = 3f;
	[SerializeField]
	private float m_MaxJump = 25f;
	[SerializeField]
	protected float m_JumpForce = 850f;
	[SerializeField]
	protected bool m_IsFacingRight = true;

	private Direction mDirection;
	
	protected bool mInitJumping = false;
	protected bool mIsGrounded;
	
	protected CharacterEdgeChecker mEdgeChecker;
	
	protected Rigidbody2D mRigidbody2D;
	
	public virtual void Start () {
		mEdgeChecker = GetComponent<CharacterEdgeChecker>();
		mRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	protected virtual void Update (){
		if (UIPopupBase.PopupIsDisplayed) return;
		
		mIsGrounded = mEdgeChecker.TouchSomething(EEdge.BOTTOM);
		
		if (CharacterWantToJump && mIsGrounded)
		{
			mInitJumping = true;
		}
		float direction = GetHorizontalAxisValue();
		Move(direction);
	}
	
	protected virtual void FixedUpdate()
	{
		if (UIPopupBase.PopupIsDisplayed) return;
	}
	
	protected virtual void Move(float pSpeed)
	{
		Direction direction = GetDirection (pSpeed);
		UpdateAnimation(direction, mIsGrounded);
		
		if (direction.Value != 0 && CanMove(direction) && !HorizontalMaxSpeedReached(direction))
		{
			AddForce(pSpeed);
		}
		
		AjustVelocity();
		CheckFlipping(direction);
		UpdateJumping();
	}
	
	protected bool CanMove(Direction pDirection)
	{
		return !(mEdgeChecker.TouchSomething(pDirection.Edge));
	}
	
	protected bool HorizontalMaxSpeedReached(Direction pDirection)
	{
		return pDirection.Value * mRigidbody2D.velocity.x >= GetMaxSpeed();
	}
	
	protected virtual void AddForce(float pSpeed)
	{
		mRigidbody2D.AddForce(Vector2.right * pSpeed * m_MoveForce);
	}
	
	protected void AjustVelocity()
	{
		Vector2 newVelocity = mRigidbody2D.velocity;
		
		if (Mathf.Abs(mRigidbody2D.velocity.x) > GetMaxSpeed())
		{
			newVelocity.x = Mathf.Sign(mRigidbody2D.velocity.x) * GetMaxSpeed();
		}
		
		if (Mathf.Abs(mRigidbody2D.velocity.y) > m_MaxJump)
		{
			newVelocity.y = Mathf.Sign(mRigidbody2D.velocity.y) * m_MaxJump;
		}
		
		mRigidbody2D.velocity = newVelocity;
	}
	
	protected void CheckFlipping(Direction pDirection)
	{
		if (pDirection.IsRightDirection() && !m_IsFacingRight
		    || pDirection.IsLeftDirection() && m_IsFacingRight)
		{
			Flip();
		}
	}
	
	protected virtual void UpdateJumping()
	{	}
	
	protected abstract float GetHorizontalAxisValue();
	
	protected abstract bool CharacterWantToJump
	{
		get;
	}

	protected Direction GetDirection(float speed)
	{
		int directionInt = 0;
		if (speed > 0) {
			directionInt = 1;
		} else if (speed < 0) {
			directionInt = -1;
		}
		return new Direction (directionInt);
	}

	protected abstract void UpdateAnimation(Direction pDirection,bool pIsGrounded);
	
	protected bool IsInAir
	{
		get { return !mIsGrounded; }
	}
	
	protected float GetMaxSpeed()
	{
		return mIsGrounded ? m_MaxSpeed : 4f;
	}
	
	protected void Flip()
	{
		m_IsFacingRight = !m_IsFacingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

