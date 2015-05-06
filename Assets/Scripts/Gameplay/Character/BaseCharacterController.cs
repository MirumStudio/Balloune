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
		var direction = new Direction(GetHorizontalAxisValue());
		Move(direction);
	}
	
	protected virtual void FixedUpdate()
	{
		if (UIPopupBase.PopupIsDisplayed) return;
	}
	
	protected virtual void Move(Direction pDirection)
	{
		UpdateAnimation(pDirection, mIsGrounded);
		
		if (pDirection.Value != 0 && CanMove(pDirection) && !HorizontalMaxSpeedReached(pDirection))
		{
			AddForce(pDirection);
		}
		
		AjustVelocity();
		CheckFlipping(pDirection);
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
	
	protected virtual void AddForce(Direction pDirection)
	{
		mRigidbody2D.AddForce(Vector2.right * pDirection.Value * m_MoveForce);
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
	
	protected abstract int GetHorizontalAxisValue();
	
	protected abstract bool CharacterWantToJump
	{
		get;
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

