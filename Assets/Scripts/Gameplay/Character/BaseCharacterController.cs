using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class BaseCharacterController : MonoBehaviour {


	private bool mIsGrounded;
    public float moveForce = 350f;
    public float maxSpeed = 3f;
    bool jump = false;
    public float jumpForce = 300f;	
    public Transform groundCheck;

	public virtual void Start () {
        groundCheck = transform.Find("GroundCheck");
	}
	
	void Update (){
        mIsGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (CharacterWantToJump && mIsGrounded)
        {
            jump = true;
        }
	}

    void FixedUpdate()
    {
        float direction = GetHorizontalAxisValue();

        UpdateAnimation(direction, mIsGrounded);

        if (direction * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * direction * moveForce);
        }

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }

        /*// If the input is moving the player right and the player is facing left...
        if (h > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (h < 0 && facingRight)
            // ... flip the player.
            Flip();*/

        if (jump)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Math.Min(rigidbody2D.velocity.y, 5.6f));
            
            jump = false;
        }
    }

	protected abstract float GetHorizontalAxisValue();
	
	protected abstract bool CharacterWantToJump
	{
		get;
	}
	
	protected bool IsInAir
	{
		get { return !mIsGrounded; }
	}
	
	protected abstract void UpdateAnimation(float pDirection,bool pIsGrounded);
}

