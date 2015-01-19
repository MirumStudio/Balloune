using UnityEngine;
using System.Collections;
using System;

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

    private bool jump = false;
    private bool mIsGrounded;

    public Transform groundCheck;
    private CharacterEdgeChecker mEdgeChecker;

	public virtual void Start () {
        groundCheck = transform.Find("GroundCheck");
        mEdgeChecker = GetComponent<CharacterEdgeChecker>();
	}
	
	void Update (){
        mIsGrounded = mEdgeChecker.TouchSomething(EEdge.BOTTOM);

        if (CharacterWantToJump && mIsGrounded)
        {
            jump = true;
        }
	}

    void FixedUpdate()
    {
        var direction = new Direction(GetHorizontalAxisValue());

        UpdateAnimation(direction, mIsGrounded);

        if (CanMove(direction) && !HorizontalMaxSpeedReached(direction))
        {
            AddForce(direction);
        }

        AjustVelocity();
        CheckFlipping();
        CheckJumping();
    }

    private bool CanMove(Direction pDirection)
    {
        return !(IsInAir && mEdgeChecker.TouchSomething(pDirection.Edge));
    }

    private bool HorizontalMaxSpeedReached(Direction pDirection)
    {
        return pDirection.Value * rigidbody2D.velocity.x >= GetMaxSpeed();
    }

    private void AddForce(Direction pDirection)
    {
        rigidbody2D.AddForce(Vector2.right * pDirection.Value * moveForce);
    }

    private void AjustVelocity()
    {
        Vector2 newVelocity = rigidbody2D.velocity;

        if (Mathf.Abs(rigidbody2D.velocity.x) > GetMaxSpeed())
        {
            newVelocity.x = Mathf.Sign(rigidbody2D.velocity.x) * GetMaxSpeed();
        }

        if (Mathf.Abs(rigidbody2D.velocity.y) > maxJump)
        {
            newVelocity.y = Mathf.Sign(rigidbody2D.velocity.y) * maxJump;
        }

        rigidbody2D.velocity = newVelocity;
    }

    private void CheckFlipping()
    {
        /*// If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();*/
    }

    private void CheckJumping()
    {
        if (jump)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

	protected abstract int GetHorizontalAxisValue();
	
	protected abstract bool CharacterWantToJump
	{
		get;
	}
	
	protected bool IsInAir
	{
		get { return !mIsGrounded; }
	}
	
	protected abstract void UpdateAnimation(Direction pDirection,bool pIsGrounded);

    protected float GetMaxSpeed()
    {
        return mIsGrounded ? maxSpeed : 4f;
    }
}

