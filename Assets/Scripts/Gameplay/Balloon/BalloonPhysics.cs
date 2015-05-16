using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.ErrorMangement;
using System.Collections.Generic;

public class BalloonPhysics : MonoBehaviour
{
	private const float MAX_DRAG_VELOCITY = 15f;
	private const float TIME_TO_DETACH = 1f;
	
	[SerializeField]
    public Transform m_Parent = null;
	
	private Balloon mBalloon = null;
	private int mBalloonIndex = -1;

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
	private bool mIsAttached = true;

	private float mTimePullingAtMaximumDistance = 0f;

    void Start()
    {
        mBalloon = GetComponent<Balloon>();
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
		ChangeTimePullingAtMaximumDistance ();
	}
	
	public void MoveBalloon()
    {
        if (mIsTouched)
        {
            Vector2 currentBalloonPosition = transform.position;
            Vector2 touchPosition = TouchControl.GetTouchPosition();
            float balloonDistance = GetDistanceBetweenParentAndPosition();
			if (IsBalloonAtMaximumDistance() == false)
			{
				DragBalloon(touchPosition, currentBalloonPosition);
				mBalloon.OnMove(balloonDistance);
			}
			if (IsBalloonAtMaximumDistance())
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

    public double GetBalloonAngle()
    {
        float deltaX = transform.position.x - mTack.transform.position.x;
        float deltaY = transform.position.y - mTack.transform.position.y;
        float radian = Mathf.Atan2(deltaY, deltaX);
        double angle = radian * Mathf.Rad2Deg;
        return angle;
    }
	
	public void DetachBalloon()
	{
		if (mTimePullingAtMaximumDistance >= TIME_TO_DETACH) {
			mDistanceJoint.enabled = false;
			mBalloonJoint.enabled = false;
			mLineRenderer.enabled = false;
			mIsAttached = false;
			mBalloonHolder.DetachBalloon(mBalloonIndex);
		}
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
    }

    private void PopBalloon()
    {
        var balloon = GetComponent<Balloon>();
        balloon.OnPop();
        mBalloonHolder.DestroyBalloon(balloon);
    }

    public void SetInvulnerable(bool pInvulnerable)
    {
        this.mIsInvulnerable = pInvulnerable;
    }

    public void SetBalloonHolder(BalloonHolder pBalloonHolder)
    {
        mBalloonHolder = pBalloonHolder;
    }

	public void SetBalloonIndex(int pBalloonIndex)
	{
		mBalloonIndex = pBalloonIndex;
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

	public CircleCollider2D GetCircleCollider()
	{
		return mCircleCollider;
	}
	
	public Rigidbody2D GetRigidBody()
    {
		return mRigidbody2D;
    }

	private bool IsBalloonAtMaximumDistance()
	{
		bool isBalloonAtMaximumDistance = false;
		if(mIsAttached)
		{
			isBalloonAtMaximumDistance = GetDistanceBetweenParentAndPosition() >= mDistanceJoint.distance;
		}
		return isBalloonAtMaximumDistance;
	}

	private bool IsBalloonAtDetachDistance()
	{
		bool isBalloonAtMaximumDistance = false;
		//We have to do this because the balloon is almost never exactly at its max distance
		float distanceOffset = mDistanceJoint.distance * 0.00025f;
		float detachMaxDistance = mDistanceJoint.distance - distanceOffset;
		if(mIsAttached)
		{
			isBalloonAtMaximumDistance = GetDistanceBetweenParentAndPosition() >= detachMaxDistance;
		}
		return isBalloonAtMaximumDistance;
	}

	private void ChangeTimePullingAtMaximumDistance()
	{
		if (IsBalloonAtDetachDistance () && mIsTouched	) {
			mTimePullingAtMaximumDistance = mTimePullingAtMaximumDistance + Time.deltaTime;
		} else {
			mTimePullingAtMaximumDistance = 0f;
		}
	}
}