using UnityEngine;
using System.Collections;

public class MainCharacterController : MonoBehaviour {
    private const float PIVOT_GROUNDED_ADJUSTEMENT = 1.2f;

    [SerializeField]
    private LayerMask m_PlatformLayerMask;
    [SerializeField]
    private float m_JumpHeight = 250;
    [SerializeField]
    private float m_WalkSpeed = 4;
    [SerializeField]
    private float m_RunSpeed = 10;
    [SerializeField]
    private float m_AirControl = 0.2f;
    [SerializeField]
    private float m_ReactionSpeed = 8;

    private bool mIsGrounded;
    private float mJumpDirection;
    private float mLastDirection;

    private CharacterAnimator mAnimator;

    void Start() {
        mAnimator = GetComponent<CharacterAnimator>();
    }

	void Update () {
        mIsGrounded = GroundHitTest();

        float direction = GetHorizontalAxisValue() * GetSpeed();

        if (mIsGrounded && PlayerWantToJump)
        {
            InitJumping(direction);
        }

        direction = AjustDirection(direction);

        Move(direction);
        SaveDirection(direction);

        mAnimator.UpdateAnimation(direction, mIsGrounded, PlayerWantToRun);
	}

    private bool GroundHitTest()
    {
        //DEBUG
        Debug.DrawLine(this.transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0), this.transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0) + Vector3.down, Color.red);

        var raycastHit2D = Physics2D.Raycast(transform.position - new Vector3(0, PIVOT_GROUNDED_ADJUSTEMENT, 0), Vector3.down);
        return raycastHit2D.distance == 0;
    }

    private bool PlayerWantToRun
    {
        get { return Input.GetKey(KeyCode.LeftShift); }
    }

    private float GetHorizontalAxisValue()
    {
        return Input.GetAxis("Horizontal");
    }
    
    private float GetSpeed()
    {
        return PlayerWantToRun ? m_RunSpeed : m_WalkSpeed;
    }

    private bool PlayerWantToJump
    {
        get { return Input.GetKey(KeyCode.Space); }
    }

    private void InitJumping(float pDirection)
    {
        mJumpDirection = pDirection;
        rigidbody2D.AddForce(Vector3.up * m_JumpHeight);
    }

    private bool IsInAir
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
}
