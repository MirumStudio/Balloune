using UnityEngine;
using System.Collections;
using Radix.Event;

[RequireComponent (typeof(CharacterAnimator))]
public class MainCharacterController : BaseCharacterController {
  
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

	protected override int GetHorizontalAxisValue() 
	{
        int value = (int)Mathf.Sign(Input.GetAxis("Horizontal"));

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
        if (pCollision.gameObject.name.Contains("kid"))
        {
            var lol = GetComponent<Collider2D>();
            var lol2 = pCollision.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), pCollision.gameObject.GetComponent<Collider2D>());
            EventService.DipatchEvent(EGameEvent.CHILD_COLLISION, this);
        }
        
    }

    /* How reset ignoreCollision
     * /*pCollision.gameObject.SetActive(false);
            pCollision.gameObject.SetActive(true);*/
    //pCollision.gameObject.SetActive(false);*/
}
