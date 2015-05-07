using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.Error;

public class BalloonBehavior : MonoBehaviour
{
    [SerializeField]
    public Transform m_Parent = null;

	public int mBalloonIndex = -1;

    private Rigidbody2D mRigidbody2D = null;
	private LineRenderer mLineRenderer = null;
	private HingeJoint2D mBalloonJoint = null;
	private BalloonHolder mBalloonHolder = null;

	private Rope mRope = null;

	[SerializeField]
	private float m_MaximumInvulnerableTime = 2f;
	private bool mIsInvulnerable = false;
	private float mInvulnerableTime = 0f;

    void Start()
    {
		mRigidbody2D = GetComponent<Rigidbody2D>();
		mLineRenderer = GetComponent<LineRenderer> ();
		mBalloonJoint = GetComponent<HingeJoint2D> ();
		mRope = mBalloonHolder.GetRope (mBalloonIndex);
		EventListener.Register(EGameEvent.HAZARDOUS_COLLISION, OnHazardousCollision);
    }

    private void Update()
    {
		CheckIfInvulnerable ();
    }

	private void FixedUpdate()
	{
		UpdateLineRenderer ();
	}

    private float GetDistanceBetweenParentAndPosition()
    {
		return Vector2.Distance(m_Parent.position, mRigidbody2D.position);
    }

	private void UpdateLineRenderer()
	{
		mRope.DrawRope (mLineRenderer, mBalloonJoint);
	}

	private void CheckIfInvulnerable() {
		if (mIsInvulnerable) {
			mInvulnerableTime += Time.deltaTime;
			if (mInvulnerableTime >= m_MaximumInvulnerableTime) {
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
			if (interactable.IsPassableThrough)
			{
				var interactableCollider = pCollision.gameObject.GetComponent<Collider2D>();
				Physics2D.IgnoreCollision(GetComponent<Collider2D>(), interactableCollider);
			}
		}
	}

	private void OnHazardousCollision(Enum pEvent, System.Object pArg)
	{
		Assert.Check(pArg is HazardousInteractable);
		int damage = (pArg as HazardousInteractable).Damage;
		if (damage > 0 && !this.mIsInvulnerable)
		{
			PopBalloon();
		}
		CheckIfGameOver();
	}

	private void PopBalloon(){
		//TODO balloon explosion animation and sound effect
		mBalloonHolder.DestroyBalloon(mBalloonIndex);
	}

	private void CheckIfGameOver() {
		if (mBalloonHolder.CountBalloons() <= 0)
		{
			EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
		}
	}
	
	public void SetInvulnerable(bool pInvulnerable) {
		this.mIsInvulnerable = pInvulnerable;
	}

	public void SetBalloonHolder(BalloonHolder pBalloonHolder)
	{
		mBalloonHolder = pBalloonHolder;
	}
}
