using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.ErrorMangement;
using System.Collections.Generic;

public class BalloonPhysics : MonoBehaviour
{
    private const float MAX_DRAG_VELOCITY = 15f;

    [SerializeField]
    public Transform m_Parent = null;

    public int mBalloonIndex = -1;

    private Rigidbody2D mRigidbody2D = null;
    private LineRenderer mLineRenderer = null;
    private HingeJoint2D mBalloonJoint = null;
    private BalloonHolder mBalloonHolder = null;
    private DistanceJoint2D mDistanceJoint = null;
    private CircleCollider2D mCircleCollider = null;
    private GameObject mTack = null;
    private Transform mainCharacter = null;

    private Rope mRope = null;

    [SerializeField]
    private float m_MaximumInvulnerableTime = 2f;
    private bool mIsInvulnerable = false;
    private float mInvulnerableTime = 0f;

    private bool mIsTouched = false;
    private CharacterPull mCharacterPull = new CharacterPull();

    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mLineRenderer = GetComponent<LineRenderer>();
        mBalloonJoint = GetComponent<HingeJoint2D>();
        mDistanceJoint = GetComponent<DistanceJoint2D>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mRope = GetComponentInChildren<Rope>();
        m_Parent = mBalloonHolder.transform;
        //EventListener.Register(EGameEvent.HAZARDOUS_COLLISION, OnHazardousCollision);
        mainCharacter = mTack.transform.parent;
        Physics2D.IgnoreCollision(mainCharacter.GetComponent<BoxCollider2D>(), mCircleCollider);
    }


    private void Update()
    {
        CheckIfInvulnerable();
    }

    private void FixedUpdate()
    {
        UpdateLineRenderer();
        MoveBalloon();
    }

    public void MoveBalloon()
    {
        if (mIsTouched)
        {
            Vector2 currentBalloonPosition = transform.position;
            Vector2 touchPosition = TouchControl.GetTouchPosition();
            float balloonDistance = GetDistanceBetweenParentAndPosition();
            if (balloonDistance < mDistanceJoint.distance)
            {
                DragBalloon(touchPosition, currentBalloonPosition);
                DragCharacter(currentBalloonPosition);
            }
            if (balloonDistance >= mDistanceJoint.distance)
            {
                SetVelocity(Vector2.zero);
            }
        }
    }

    public void IgnoreOtherBalloonCollision(bool pIgnore)
    {
        List<Balloon> allHeldballoons = mBalloonHolder.Ballounes;

        for (int i = 0; i < allHeldballoons.Count; i++)
        {
            if (i != mBalloonIndex)
            {
                Physics2D.IgnoreCollision(allHeldballoons[i].CircleCollider, mCircleCollider, pIgnore);
            }
        }
    }

    private void DragBalloon(Vector2 pTouchPosition, Vector2 pCurrentBalloonPosition)
    {
        float xDistance = pTouchPosition.x - pCurrentBalloonPosition.x;
        float yDistance = pTouchPosition.y - pCurrentBalloonPosition.y;
        Vector2 velocity = new Vector2(xDistance * 5, yDistance * 5);
        SetVelocity(velocity);
    }

    private void DragCharacter(Vector2 pCurrentBalloonPosition)
    {
        double balloonAngle = GetBalloonAngle();
        mCharacterPull.SetPullStrength(balloonAngle);
        float mDirection = mCharacterPull.GetPullDirection();
    }

    private double GetBalloonAngle()
    {
        float deltaX = transform.position.x - mTack.transform.position.x;
        float deltaY = transform.position.y - mTack.transform.position.y;
        float radian = Mathf.Atan2(deltaY, deltaX);
        double angle = radian * Mathf.Rad2Deg;
        return angle;
    }

    private void SetVelocity(Vector2 velocity)
    {
        velocity = Vector2.ClampMagnitude(velocity, MAX_DRAG_VELOCITY);
        mRigidbody2D.velocity = velocity;
    }

    private float GetDistanceBetweenParentAndPosition()
    {
        return Vector2.Distance(mTack.transform.position, transform.TransformPoint(mDistanceJoint.anchor));
    }

    private void UpdateLineRenderer()
    {
        mRope.DrawRope(mLineRenderer, mBalloonJoint);
    }

    private void CheckIfInvulnerable()
    {
        if (mIsInvulnerable)
        {
            mInvulnerableTime += Time.deltaTime;
            if (mInvulnerableTime >= m_MaximumInvulnerableTime)
            {
                mIsInvulnerable = false;
                mInvulnerableTime = 0;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D pCollision)
    {
        var interactable = pCollision.gameObject.GetComponent<Interactable>();
        if (interactable != null && interactable.GetType() == typeof(HazardousInteractable))
        {
            interactable.DispacthEvent();
            OnHazardousCollision(interactable as HazardousInteractable);
            if (interactable.IsPassableThrough)
            {
                var interactableCollider = pCollision.gameObject.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), interactableCollider);
            }
        }
    }

    private void OnHazardousCollision(HazardousInteractable pArg)
    {
        int damage = pArg.Damage;
        if (damage > 0 && !this.mIsInvulnerable)
        {
            PopBalloon();
        }
        CheckIfGameOver();
    }

    private void PopBalloon()
    {
        //TODO balloon explosion animation and sound effect

        mBalloonHolder.DestroyBalloon(GetComponent<Balloon>());
    }

    private void CheckIfGameOver()
    {
        if (mBalloonHolder.CountBalloons() <= 0)
        {
            EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
        }
    }

    public void SetInvulnerable(bool pInvulnerable)
    {
        this.mIsInvulnerable = pInvulnerable;
    }

    public void SetBalloonHolder(BalloonHolder pBalloonHolder)
    {
        mBalloonHolder = pBalloonHolder;
    }

    public void SetTack(GameObject pTack)
    {
        mTack = pTack;
    }

    public void setIsTouched(bool pIsTouched)
    {
        mIsTouched = pIsTouched;
    }

    public bool IsTouched()
    {
        return mIsTouched;
    }

    public CharacterPull GetPull()
    {
        return mCharacterPull;
    }

    public bool IsPullingCharacter()
    {
        bool isPulling = false;
        if (mCharacterPull != null && mCharacterPull.IsPulling())
        {
            isPulling = true;
        }
        return isPulling;
    }

    public CircleCollider2D GetCircleCollider()
    {
        return mCircleCollider;
    }

    public Rigidbody2D GetRigidBody()
    {
        return mRigidbody2D;
    }
}