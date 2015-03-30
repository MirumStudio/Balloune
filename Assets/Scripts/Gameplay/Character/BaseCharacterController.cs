using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class BaseCharacterController : MonoBehaviour 
{
	[SerializeField]
    private float moveForce = 350f;
    [SerializeField]
    private float maxSpeed = 3f;
    [SerializeField]
    private float maxJump = 5.6f;
    [SerializeField]
    private float jumpForce = 300f;
	[SerializeField]
	private float boostedJumpForce = 300f;
	[SerializeField]
	private float mTimePressed = 0;

    private bool mIsFacingRight = true;	

	private bool mInitJumping = false;
	private bool mCanBoostJump = false;
    private bool mIsGrounded;

    private CharacterEdgeChecker mEdgeChecker;

    private Rigidbody2D mRigidbody2D;

	public virtual void Start () {
        mEdgeChecker = GetComponent<CharacterEdgeChecker>();
        mRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update (){
        if (UIPopupBase.PopupIsDisplayed) return;

        mIsGrounded = mEdgeChecker.TouchSomething(EEdge.BOTTOM);

        if (CharacterWantToJump && mIsGrounded)
        {
			mInitJumping = true;
			mCanBoostJump = true;
			mTimePressed = 0;
        }
	}

    void FixedUpdate()
    {
        if (UIPopupBase.PopupIsDisplayed) return;

        var direction = new Direction(GetHorizontalAxisValue());

        UpdateAnimation(direction, mIsGrounded);

        if (direction.Value != 0 && CanMove(direction) && !HorizontalMaxSpeedReached(direction))
        {
            AddForce(direction);
        }

        AjustVelocity();
        CheckFlipping(direction);
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

    private void AddForce(Direction pDirection)
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
        if (pDirection.IsRightDirection() && !mIsFacingRight
            || pDirection.IsLeftDirection() && mIsFacingRight)
        {
            Flip();
        }
    }

	private void UpdateJumping()
	{
		mTimePressed = mTimePressed + Time.deltaTime;
		if (CharacterWantToJump) {
			if (mInitJumping)
			{
				//Character is on the ground
				mRigidbody2D.AddForce(new Vector2(0f, jumpForce));
				mInitJumping = false;
			}
			else if(mTimePressed > 0.1f && mTimePressed < 0.15f && mCanBoostJump){
				//Character is already in the air
				mRigidbody2D.AddForce(new Vector2(0f, boostedJumpForce));
				mTimePressed = 0;
				mCanBoostJump = false;
			}
		}
	}

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
        mIsFacingRight = !mIsFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

