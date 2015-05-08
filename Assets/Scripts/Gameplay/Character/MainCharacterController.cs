using UnityEngine;
using System.Collections;
using Radix.Event;

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
	
	public GameObject m_RightButton;
	public GameObject m_LeftButton;
	public GameObject m_JumpButton;

	[SerializeField]
	private float m_BoostedJumpForce = 300f;
	
	private bool mCanBoostJump = false;
	private float mTimeJumpPressed = 0;
	
	private CharacterAnimator mAnimator;
	
	public override void Start ()
	{
		base.Start ();
		mAnimator=GetComponent<CharacterAnimator>();
	}
	
	protected override void Update (){
		base.Update ();
		
		if (CharacterWantToJump && base.mIsGrounded)
		{
			mCanBoostJump = true;
			mTimeJumpPressed = 0;
		}
	}
	
	protected override void FixedUpdate() 
	{
		base.FixedUpdate ();
	}
	
	protected override bool CharacterWantToJump 
	{
		get { return SwipeControl.IsJumpCommand();/*return Input.GetKey(KeyCode.Space) || (m_JumpButton != null && m_JumpButton.GetComponent<ButtonOnPressed>().IsPressed)*/;}
	}
	
	protected override void UpdateJumping()
	{
		mTimeJumpPressed = mTimeJumpPressed + Time.deltaTime;
		if (CharacterWantToJump) {
			Jump ();
		}
	}
	
	private void Jump(){
		if (base.mInitJumping)
		{
			//Character is on the ground
			mRigidbody2D.AddForce(new Vector2(0f, base.m_JumpForce));
			mInitJumping = false;
		}
		else if(mTimeJumpPressed > 0.1f && mTimeJumpPressed < 0.15f && mCanBoostJump){
			BoostJump ();
		}
	}
	
	private void BoostJump(){
		//Character is already in the air
		mRigidbody2D.AddForce(new Vector2(0f, m_BoostedJumpForce));
		mTimeJumpPressed = 0;
		mCanBoostJump = false;
	}
	
	protected override int GetHorizontalAxisValue() 
	{
		//float value = Input.GetAxis("Horizontal");
		float value = SwipeControl.GetDirection ();
		/*if(value == 0)
		{
			if(m_RightButton != null && m_RightButton.GetComponent<ButtonOnPressed>().IsPressed)
			{
				value++;
			}
			else if (m_LeftButton != null && m_LeftButton.GetComponent<ButtonOnPressed>().IsPressed)
			{
				value--;
			}
			return (int)value;
		}*/
		
		return (int)value;
	}
	
	private bool PlayerWantToRun
	{
		get { return Input.GetKey(KeyCode.LeftShift)/* || RunButton.GetComponent<ButtonOnPressed>().IsPressed*/; }
	}
	
	private bool PlayerWantToJump
	{
		get { return Input.GetKey(KeyCode.Space); }
	}
	
	protected override void UpdateAnimation(Direction pDirection, bool pIsGrounded)
	{
		this.mAnimator.UpdateAnimation(pDirection,pIsGrounded,PlayerWantToRun);
	}
	
	public void OnCollisionEnter2D(Collision2D pCollision)
	{
		var interactable = pCollision.gameObject.GetComponent<Interactable>();
		if (interactable != null && interactable.GetType() != typeof(HazardousInteractable))
		{
			interactable.DispacthEvent();
			if (interactable.IsPassableThrough)
			{
				var interactableCollider = pCollision.gameObject.GetComponent<Collider2D>();
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), interactableCollider);
			}
		}
	}

    /* FOR REFERENCE
     * Physics2D.IgnoreCollision(GetComponent<Collider2D>(), interactableCollider, false);
    How reset ignoreCollision
     * /*pCollision.gameObject.SetActive(false);
            pCollision.gameObject.SetActive(true);*/
    //pCollision.gameObject.SetActive(false);*/
}
