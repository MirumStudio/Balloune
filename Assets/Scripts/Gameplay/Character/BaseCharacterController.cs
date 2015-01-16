using UnityEngine;
using System.Collections;
using System;

[RequireComponent (typeof(Rigidbody2D))]
//[RequireComponent (typeof(RectCollider))]
public abstract class BaseCharacterController : MonoBehaviour {
	[SerializeField]
	private float m_WalkSpeed = 4;
	[SerializeField]
	private float m_JumpHeight = 250;
	[SerializeField]
	private float m_AirControl = 0.2f;
	[SerializeField]
	private float m_ReactionSpeed = 8;
	[SerializeField]
	private bool m_FullHorizontalControl=true;

	private float mLastXPos;
	private bool mIsGrounded;
	private float mJumpDirection;
	private float mLastDirection;
	private LayerMask mUsedLayerMask;
	//private RectCollider mRectCollider;
    public float moveForce = 350f;			// Amount of force added to move the player left and right.
    public float maxSpeed = 3f;
    bool jump = false;
    public float jumpForce = 1000f;	
    public Transform groundCheck;
    public Transform lol3;
	public virtual void Start () {
		//mRectCollider = GetComponent<RectCollider>();
		mLastXPos=transform.position.x;
        groundCheck = transform.Find("GroundCheck");
        lol3 = transform.Find("TestLol");
	}
	
	void Update (){
        mIsGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
       // Debug.Log("mIsGrounded: " + mIsGrounded);
        // If the jump button is pressed and the player is grounded then the player should jump.
        if (CharacterWantToJump && mIsGrounded)
            jump = true;
        //mIsGrounded = true;// mRectCollider.TouchSomething(RectCollider.Edges.Ground);
		//Physics2D.Linecast()
		/*float direction = GetHorizontalAxisValue() * GetSpeed();

		m_FullHorizontalControl=mIsGrounded;
		if (mIsGrounded && CharacterWantToJump)
		{
			InitJumping(direction);
		}
		else if(CharacterWantToJump)
		{
			//if(IsDirectionObstructed(direction))
			{
				InitJumping(-direction);
			}
		}
		
		direction = AjustDirection(direction);

		BlockUpward();
		if(m_FullHorizontalControl)
		{
			LockXAxis();
		}
        if (direction != 0f)
        {
            Move(direction);
            SaveDirection(direction);
        }

		UpdateAnimation(direction, mIsGrounded);*/
	}

    void FixedUpdate()
    {
        //Debug.Log(Time.deltaTime);
        float h = GetHorizontalAxisValue();
       // Debug.Log(h);
        UpdateAnimation(0, mIsGrounded);
        // The Speed animator parameter is set to the absolute value of the horizontal input.
       // anim.SetFloat("Speed", Mathf.Abs(h));

        if(mIsGrounded)
        {
            //rigidbody.AddForce(new Vector2(rigidbody.velocity.x * -0.04f, 0));
        }

        bool test = Physics2D.Linecast(new Vector2(transform.position.x, lol3.position.y), lol3.position, 1 << LayerMask.NameToLayer("Ground"));
        // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (!test && h * rigidbody2D.velocity.x < maxSpeed)
        {
            // ... add a force to the player.
            rigidbody2D.AddForce(new Vector2(Vector2.right.x * h * moveForce, 0));
           // transform.Translate(GetSpeed() * h * Time.deltaTime * Vector3.right);
        }

        // If the player's horizontal velocity is greater than the maxSpeed...
        if (!test && Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            // ... set the player's velocity to the maxSpeed in the x axis.
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

        // If the player should jump...
        if (jump)
        {
            /*// Set the Jump animator trigger parameter.
            anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);*/

            // Add a vertical force to the player.
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Math.Min(rigidbody2D.velocity.y, 5.6f));
            

            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
        Debug.Log(rigidbody2D.velocity);
    }

	protected abstract float GetHorizontalAxisValue();
	
	protected virtual float GetSpeed()
	{
		return WalkSpeed;
	}
	
	protected float WalkSpeed
	{
		get{return m_WalkSpeed;}
	}
	
	protected abstract bool CharacterWantToJump
	{
		get;
	}
	
	private void InitJumping(float pDirection)
	{
		mJumpDirection = pDirection;
		rigidbody2D.velocity=new Vector2(pDirection/20,1) * m_JumpHeight;
	}
	
	protected bool IsInAir
	{
		get { return !mIsGrounded; }
	}
	
	private float AjustDirection(float pDirection)
	{
		float ajustedDirection = pDirection;
		if (IsInAir)
		{
			//On garde une partie de la direction que l'on avait quand on sautaut (dépend de JumpDirection)
			ajustedDirection = Mathf.Lerp(mJumpDirection, pDirection, m_AirControl);
		}
		
		if (ajustedDirection != 0)
		{
			//On interpole la direction de la derniere frame et de celle-ci, afin d'éviter des changements trop brusques.
			ajustedDirection = Mathf.Lerp(this.mLastDirection, ajustedDirection, Time.deltaTime * m_ReactionSpeed);
		}
		
		return ajustedDirection;
	}
	
	private void Move(float pDirection)
	{
		//if(!IsDirectionObstructed(pDirection))
		{
            //rigidbody2D.MovePosition(rigidbody2D.position + pDirection * Time.deltaTime);
			transform.Translate(pDirection * Time.deltaTime * Vector3.right);
			mLastXPos=transform.position.x;
		}
		if(!m_FullHorizontalControl)
		{
			rigidbody2D.velocity=new Vector2(Mathf.Lerp(rigidbody2D.velocity.x,pDirection,Time.deltaTime*m_AirControl),rigidbody2D.velocity.y);
		}
	}

	private void BlockUpward()
	{
		/*if(mRectCollider.TouchSomething(RectCollider.Edges.Top))
		{
			rigidbody2D.velocity= new Vector2(rigidbody2D.velocity.x,rigidbody2D.velocity.y>0?0:rigidbody2D.velocity.y);
		}*/
	}

	private void LockXAxis()
	{
		transform.position=new Vector3(mLastXPos,transform.position.y,transform.position.z);
	}

	private void SaveDirection(float pDirection)
	{
		mLastDirection = pDirection;
	}

	/*private bool IsDirectionObstructed(float pDirection)
	{
        //TODO CLEAN THIS CODE
        if (pDirection > 0)
        {
            RaycastHit2D hit = mRectCollider.GetEdgeRayCastHit(RectCollider.Edges.Right);
            if (hit != null && hit.transform != null && hit.transform.name.Contains("kid"))
            {
                return false;
            }
        }
        else if (pDirection < 0)
        {
            RaycastHit2D hit = mRectCollider.GetEdgeRayCastHit(RectCollider.Edges.Left);
            if (hit != null && hit.transform != null && hit.transform.name.Contains("kid"))
            {
                return false;
            }
        }

        return pDirection > 0 && mRectCollider.TouchSomething(RectCollider.Edges.Right)
             || pDirection < 0 && mRectCollider.TouchSomething(RectCollider.Edges.Left);
	}*/
	
	protected abstract void UpdateAnimation(float pDirection,bool pIsGrounded);
}

