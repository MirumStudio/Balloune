using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class BaseCharacterController : MonoBehaviour {
	[SerializeField]
	private float m_PivotGroundedAdjustement = 1.2f;
	[SerializeField]
	private float m_GroundedBaseWidth = 1f;
	[SerializeField]
	private int m_GroundTestNumberOfInnerRayCasts = 1;
	[SerializeField]
	private float m_WalkSpeed = 4;
	[SerializeField]
	private float m_JumpHeight = 250;
	[SerializeField]
	private float m_AirControl = 0.2f;
	[SerializeField]
	private float m_ReactionSpeed = 8;
	
	private bool mIsGrounded;
	private float mJumpDirection;
	private float mLastDirection;
	private LayerMask mUsedLayerMask;
	
	
	void Update () {
		mIsGrounded = GroundHitTest();
		
		float direction = GetHorizontalAxisValue() * GetSpeed();
		
		if (mIsGrounded && CharacterWantToJump)
		{
			InitJumping(direction);
		}
		
		direction = AjustDirection(direction);
		
		Move(direction);
		SaveDirection(direction);
		
		UpdateAnimation(direction, mIsGrounded);
	}
	
	private bool GroundHitTest()
	{
		Vector3 pivot = this.transform.position - new Vector3(m_GroundedBaseWidth/2, m_PivotGroundedAdjustement, 0);
		for(int i = 0;i<m_GroundTestNumberOfInnerRayCasts+2;i++)
		{
			//DEBUG
			Debug.DrawLine(pivot,pivot + Vector3.down, Color.red);
			var raycastHit2D = Physics2D.Raycast(pivot, Vector3.down);
			if(raycastHit2D.distance == 0){return true;}
			pivot.x+=m_GroundedBaseWidth/(m_GroundTestNumberOfInnerRayCasts+1);
		}
		return false;
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
		rigidbody2D.velocity=Vector3.up * m_JumpHeight;
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
		transform.Translate(pDirection * Time.deltaTime * Vector3.right);
	}
	
	private void SaveDirection(float pDirection)
	{
		mLastDirection = pDirection;
	}
	
	protected abstract void UpdateAnimation(float pDirection,bool pIsGrounded);
}

