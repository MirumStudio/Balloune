using UnityEngine;
using System.Collections;
using Radix.Event;
using System.Collections.Generic;

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
	
	public GameObject m_RightButton;
	public GameObject m_LeftButton;
	public GameObject m_JumpButton;

	[SerializeField]
	private float m_BoostedJumpForce = 300f;
	
	private bool mCanBoostJump = false;
	private float mTimeJumpPressed = 0;

	private BalloonHolder mBalloonHolder = null;
	
	private CharacterAnimator mAnimator;
	
	public override void Start ()
	{
		base.Start ();
		mAnimator=GetComponent<CharacterAnimator>();
		mBalloonHolder = GameObject.Find (BalloonHolder.BALLOON_HOLDER_NAME).GetComponent<BalloonHolder>();
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
		get { return TouchControl.IsJumpCommand ();/*return Input.GetKey(KeyCode.Space) || (m_JumpButton != null && m_JumpButton.GetComponent<ButtonOnPressed>().IsPressed);*/}
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
	
	protected override float GetHorizontalAxisValue() 
	{
		float speed = 0f;
        List<BalloonBehavior> lifeBalloons = mBalloonHolder.GetLifeBalloonsBehavior();
		for (int i = 0; i < lifeBalloons.Count; i++) {
			if(lifeBalloons[i] != null && lifeBalloons[i].IsPullingCharacter() == true)
			{
				CharacterPull pull = lifeBalloons[i].GetPull();
				speed = pull.GetPullStrength();
				speed = AdjustSpeed(speed);
				break;
			}
		}
		return speed;
	}

	private float AdjustSpeed(float mSpeed)
	{
		if (mSpeed < 0.10f && mSpeed > 0) {
			mSpeed = 0;
		} else if (mSpeed > -0.10f && mSpeed < 0) {
			mSpeed = -0;
		}
		return mSpeed;
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
