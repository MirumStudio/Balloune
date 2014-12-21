using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(RectCollider))]
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
	private RectCollider mRectCollider;

	public virtual void Start () {
		mRectCollider = GetComponent<RectCollider>();
		mLastXPos=transform.position.x;
	}
	
	void Update (){
		mIsGrounded = mRectCollider.TouchSomething(RectCollider.Edges.Ground);
		
		float direction = GetHorizontalAxisValue() * GetSpeed();
		m_FullHorizontalControl=mIsGrounded;
		if (mIsGrounded && CharacterWantToJump)
		{
			InitJumping(direction);
		}
		else if(CharacterWantToJump)
		{
			if(IsDirectionObstructed(direction))
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
		Move(direction);
		SaveDirection(direction);
		
		UpdateAnimation(direction, mIsGrounded);
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
		if(!IsDirectionObstructed(pDirection))
		{
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
		if(mRectCollider.TouchSomething(RectCollider.Edges.Top))
		{
			rigidbody2D.velocity= new Vector2(rigidbody2D.velocity.x,rigidbody2D.velocity.y>0?0:rigidbody2D.velocity.y);
		}
	}

	private void LockXAxis()
	{
		transform.position=new Vector3(mLastXPos,transform.position.y,transform.position.z);
	}

	private void SaveDirection(float pDirection)
	{
		mLastDirection = pDirection;
	}

	private bool IsDirectionObstructed(float pDirection)
	{
		return pDirection>0 && mRectCollider.TouchSomething(RectCollider.Edges.Right) 
			|| pDirection<0 && mRectCollider.TouchSomething(RectCollider.Edges.Left);
	}
	
	protected abstract void UpdateAnimation(float pDirection,bool pIsGrounded);
}

