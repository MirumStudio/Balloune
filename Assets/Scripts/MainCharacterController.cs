using UnityEngine;
using System.Collections;

public delegate void OnKidHitHandler();

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
  
    public event OnKidHitHandler OnKidHit;

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
		get {return Input.GetKey(KeyCode.Space);}
	}

	protected override float GetHorizontalAxisValue() 
	{
		return Input.GetAxis("Horizontal");
	}

    private bool PlayerWantToRun
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
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
            pCollision.gameObject.SetActive(false);
            OnKidHit();
        }
        
    }
}
