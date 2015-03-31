using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class BaseCharacterController : MonoBehaviour 
{
	[SerializeField]
	protected float moveForce = 350f;
	[SerializeField]
	private float maxSpeed = 3f;
	[SerializeField]
	private float maxJump = 5.6f;
	[SerializeField]
	protected float jumpForce = 300f;
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
	}
	
	protected virtual void FixedUpdate()
	{
		if (UIPopupBase.PopupIsDisplayed) return;
		var direction = new Direction(GetHorizontalAxisValue());
		move (direction);
	}
	
	protected void move(Direction pDirection)
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
	
	private bool CanMove(Direction pDirection)
	{
		return !(mEdgeChecker.TouchSomething(pDirection.Edge));
	}
	
	private bool HorizontalMaxSpeedReached(Direction pDirection)
	{
		return pDirection.Value * mRigidbody2D.velocity.x >= GetMaxSpeed();
	}
	
	protected virtual void AddForce(Direction pDirection)
	{
		mRigidbody2D.AddForce(Vector2.right * pDirection.Value * moveForce);
	}
	
	private void AjustVelocity()
	{
		Vector2 newVelocity = mRigidbody2D.velocity;
		
		if (Mathf.Abs(mRigidbody2D.velocity.x) > GetMaxSpeed())
		{
			newVelocity.x = Mathf.Sign(mRigidbody2D.velocity.x) * GetMaxSpeed();
		}
		
		if (Mathf.Abs(mRigidbody2D.velocity.y) > maxJump)
		{
			newVelocity.y = Mathf.Sign(mRigidbody2D.velocity.y) * maxJump;
		}
		
		mRigidbody2D.velocity = newVelocity;
	}
	
	private void CheckFlipping(Direction pDirection)
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
		return mIsGrounded ? maxSpeed : 4f;
	}
	
	private void Flip()
	{
		m_IsFacingRight = !m_IsFacingRight;
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

