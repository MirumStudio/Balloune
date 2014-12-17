using UnityEngine;
using System.Collections;

public class CharacterControllerAnimator : MonoBehaviour {

    [SerializeField]
    private int m_Speed = 1;

    //Constantes
    private const float PIVOT_GROUNDED_ADJUSTEMENT = 1.2f;
    [SerializeField]
    private LayerMask m_PlatformLayerMask;

    //Inspector
    public float m_JumpHeight = 250;
    public float m_WalkSpeed = 4;
    public float m_RunSpeed = 10;
    public float m_AirControl = 0.2f;
    public float m_ReactionSpeed = 8;

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

    private Animator animator;
	// Use this for initialization
	void Start () {
        this.mJumpHeight = this.m_JumpHeight;
        this.mWalkSpeed = this.m_WalkSpeed;
        this.mRunSpeed = this.m_RunSpeed;
        this.mAirControl = this.m_AirControl;
        this.mReactionSpeed = this.m_ReactionSpeed;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
      /*  if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-(Vector2.right * Time.deltaTime * m_Speed));
            animator.SetInteger("Direction", 0);
            animator.speed = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("Direction", 1);
            animator.speed = 1f;
            transform.Translate(Vector2.right * Time.deltaTime * m_Speed);
        }
        else
        {
            animator.speed = 0f;
        }*/
		//CECI EST POUR LE DEBUG V
		Debug.DrawLine(this.transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0),this.transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0)+Vector3.down,Color.red);

		RaycastHit2D rh = Physics2D.Raycast(this.transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0), Vector3.down);
        this.mIsGrounded = rh.distance < 0.1;
		Debug.Log(rh.transform.gameObject.name);
        //this.mIsGrounded = this.transform.position.y < 327.1f;
        float speed;
        float direction;

        if (Input.GetKey(KeyCode.LeftShift))//Si on cours, on prend la vitesse de course.
            speed = this.mRunSpeed;
        else
            speed = this.mWalkSpeed;
        direction = Input.GetAxis("Horizontal") * speed;

        if (this.mIsGrounded)//Si on touche au sol.
        {
            if (Input.GetKey(KeyCode.Space))//On peut sauter.
            {
                this.mJumpDirection = direction; //On mémorise la direction au moment du saut.
                this.rigidbody2D.AddForce(Vector3.up * this.mJumpHeight);
            }
        }
        else //On est dans les airs!
        {//On garde une partie de la direction que l'on avait quand on sautaut (dépend de JumpDirection)
            direction = Mathf.Lerp(mJumpDirection, direction, mAirControl);
        }
        //On interpole la direction de la derniere frame et de celle-ci, afin d'éviter des changements trop brusques.

        if (direction != 0)
        {
            direction = Mathf.Lerp(this.mLastDirection, direction, Time.deltaTime * mReactionSpeed);
        }
        //Finalement on bouge!
        this.transform.Translate(direction * Time.deltaTime * Vector3.right);
        this.mLastDirection = direction;

        if (direction < 0 && mIsGrounded)
        {
            animator.SetInteger("Direction", 0);
            animator.speed = 1f;
        }
        else if (direction > 0 && mIsGrounded)
        {
            animator.SetInteger("Direction", 1);
            animator.speed = 1f;
        }
        else
        {
            animator.speed = 0f;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, m_PlatformLayerMask))
        {
            float distanceToGround = hit.distance;
            print(distanceToGround);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        int y = 6;
    }
}
