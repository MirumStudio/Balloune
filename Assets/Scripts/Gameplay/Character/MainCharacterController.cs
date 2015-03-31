using UnityEngine;
using System.Collections;
using Radix.Event;

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
	
	public GameObject RightButton;
	public GameObject LeftButton;
	public GameObject JumpButton;
	//public GameObject RunButton;
	
	[SerializeField]
	private float m_RunSpeed = 10;
	[SerializeField]
	private float m_BoostedJumpForce = 300f;
	
	private bool mCanBoostJump = false;
	private float mTimePressed = 0;
	
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
			mTimePressed = 0;
		}
	}
	
	protected override void FixedUpdate() 
	{
		base.FixedUpdate ();
	}
	
	protected override bool CharacterWantToJump 
	{
		get { return Input.GetKey(KeyCode.Space) || (JumpButton != null && JumpButton.GetComponent<ButtonOnPressed>().IsPressed);}
	}
	
	protected override void UpdateJumping()
	{
		mTimePressed = mTimePressed + Time.deltaTime;
		if (CharacterWantToJump) {
			jump ();
		}
	}
	
	private void jump(){
		if (base.mInitJumping)
		{
			//Character is on the ground
			mRigidbody2D.AddForce(new Vector2(0f, base.jumpForce));
			mInitJumping = false;
		}
		else if(mTimePressed > 0.1f && mTimePressed < 0.15f && mCanBoostJump){
			boostJump ();
		}
	}
	
	private void boostJump(){
		//Character is already in the air
		mRigidbody2D.AddForce(new Vector2(0f, m_BoostedJumpForce));
		mTimePressed = 0;
		mCanBoostJump = false;
	}
	
	protected override int GetHorizontalAxisValue() 
	{
		float value = Input.GetAxis("Horizontal");
		
		if(value == 0)
		{
			if(RightButton != null && RightButton.GetComponent<ButtonOnPressed>().IsPressed)
			{
				value++;
			}
			else if (LeftButton != null && LeftButton.GetComponent<ButtonOnPressed>().IsPressed)
			{
				value--;
			}
			return (int)value;
		}
		
		return (int)Mathf.Sign(value);
	}
	
	private bool PlayerWantToRun
	{
		get { return Input.GetKey(KeyCode.LeftShift)/* || RunButton.GetComponent<ButtonOnPressed>().IsPressed*/; }
	}
	
	/*protected override float GetSpeed()
    {
        return PlayerWantToRun ? m_RunSpeed : WalkSpeed;
    }*/
	
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
		if (interactable != null)
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
	How reset ignoreCollision
     * /*pCollision.gameObject.SetActive(false);
            pCollision.gameObject.SetActive(true);*/
	//pCollision.gameObject.SetActive(false);*/
}
