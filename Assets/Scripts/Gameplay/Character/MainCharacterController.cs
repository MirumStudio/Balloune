using UnityEngine;
using System.Collections;
using Radix.Event;

public delegate void OnKidHitHandler();

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
  
    public event OnKidHitHandler OnKidHit;

    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject JumpButton;
    public GameObject RunButton;

    [SerializeField]
    private float m_RunSpeed = 10;
	
    private CharacterAnimator mAnimator;

	public override void Start ()
	{
		base.Start ();
		mAnimator=GetComponent<CharacterAnimator>();
	}
		
	protected override bool CharacterWantToJump 
	{
		get {return Input.GetKey(KeyCode.Space) || JumpButton.GetComponent<ButtonOnPressed>().buttonHeld;}
	}

	protected override float GetHorizontalAxisValue() 
	{
        float value = Input.GetAxis("Horizontal");

        if(value == 0)
        {
            if(RightButton.GetComponent<ButtonOnPressed>().buttonHeld)
            {
                value++;
            }
            else if (LeftButton.GetComponent<ButtonOnPressed>().buttonHeld)
            {
                value--;
            }
        }


        return value ;
	}

    private bool PlayerWantToRun
    {
        get { return Input.GetKey(KeyCode.LeftShift) || RunButton.GetComponent<ButtonOnPressed>().buttonHeld; }
    }
    
	protected override float GetSpeed()
    {
        return PlayerWantToRun ? m_RunSpeed : WalkSpeed;
    }
	
    private bool PlayerWantToJump
    {
        get { return Input.GetKey(KeyCode.Space); }
    }

	protected override void UpdateAnimation (float pDirection, bool pIsGrounded)
	{
		this.mAnimator.UpdateAnimation(pDirection,pIsGrounded,PlayerWantToRun);
	}

    public void OnCollisionEnter2D(Collision2D pCollision)
    {
        if (pCollision.gameObject.name.Contains("kid") && OnKidHit != null)
        {
            //pCollision.gameObject.SetActive(false);
            OnKidHit();
            EventService.DipatchEvent(EGameEvent.TEST, this);
        }
        
    }
}
