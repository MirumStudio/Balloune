using UnityEngine;
using System.Collections;

public class CharacterControllerAlt : MonoBehaviour {

	//Constantes
	private const float PIVOT_GROUNDED_ADJUSTEMENT = 0.01f;

	//Inspector
	public float m_JumpHeight=200;
	public float m_WalkSpeed= 1;
	public float m_RunSpeed=2;
	public float m_AirControl=0.5f;
	public float m_ReactionSpeed=1;

	//Transfert from Inspector
	private float mJumpHeight;
	private float mWalkSpeed;
	private float mRunSpeed;
	private float mAirControl;
	private float mReactionSpeed;

	//Private
	private bool mIsGrounded;//If the character touches the ground.
	private float mJumpDirection;//Direction of the character when starting his last jump.
	private float mLastDirection;//Direction of the character at the previous frame.
	private Animator mAnimator;

	void Start()
	{
		//Transfert from inspector.
		this.mJumpHeight=this.m_JumpHeight;
		this.mWalkSpeed=this.m_WalkSpeed;
		this.mRunSpeed=this.m_RunSpeed;
		this.mAirControl=this.m_AirControl;
		this.mReactionSpeed=this.m_ReactionSpeed;
		this.mAnimator=this.GetComponentInChildren<Animator>();
	}

	void Update () 
	{
		this.mIsGrounded = Physics2D.Raycast(this.transform.position-new Vector3(0,PIVOT_GROUNDED_ADJUSTEMENT,0),Vector3.down,PIVOT_GROUNDED_ADJUSTEMENT);
	
		float speed;
		float direction;

		if(Input.GetButton("Fire2"))//Si on cours, on prend la vitesse de course.
			speed = this.mRunSpeed;
		else
			speed = this.mWalkSpeed;
		direction = Input.GetAxis("Horizontal")*speed;

		if(this.mIsGrounded)//Si on touche au sol.
		{
			if(Input.GetButtonDown("Fire1"))//On peut sauter.
			{
				this.mJumpDirection = direction; //On mémorise la direction au moment du saut.
				this.rigidbody2D.AddForce(Vector3.up*this.mJumpHeight);
			}
		}
		else //On est dans les airs!
		{//On garde une partie de la direction que l'on avait quand on sautaut (dépend de JumpDirection)
			direction=Mathf.Lerp(mJumpDirection,direction,mAirControl);
		}
		//On interpole la direction de la derniere frame et de celle-ci, afin d'éviter des changements trop brusques.
		direction=Mathf.Lerp(this.mLastDirection,direction,Time.deltaTime*mReactionSpeed);
		//Finalement on bouge!
		this.transform.Translate(direction * Time.deltaTime * Vector3.right);
		this.mLastDirection=direction;
		//On passe l'info a l'animator.
		this.mAnimator.SetFloat("HorizontalMove",direction);
	}
}
