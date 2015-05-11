using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.ErrorMangement;

public class BalloonBehavior : MonoBehaviour
{
    [SerializeField]
    public Transform m_Parent = null;

	public int mBalloonIndex = -1;

    private Rigidbody2D mRigidbody2D = null;
	private LineRenderer mLineRenderer = null;
	private HingeJoint2D mBalloonJoint = null;
	private BalloonHolder mBalloonHolder = null;
	private DistanceJoint2D mDistanceJoint = null;
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
		mLineRenderer = GetComponent<LineRenderer> ();
		mBalloonJoint = GetComponent<HingeJoint2D> ();
		mDistanceJoint = GetComponent<DistanceJoint2D> ();
		mRope = mBalloonHolder.GetRope (mBalloonIndex);
		m_Parent = mBalloonHolder.transform;
		EventListener.Register(EGameEvent.HAZARDOUS_COLLISION, OnHazardousCollision);
		mainCharacter = mTack.transform.parent;
		Physics2D.IgnoreCollision(mainCharacter.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
    }

	
	private void Update()
	{
		CheckIfInvulnerable ();
	}
	
	private void FixedUpdate()
	{
		UpdateLineRenderer ();
		MoveBalloon ();
	}

	public void MoveBalloon()
	{
		if (mIsTouched) {
			mRigidbody2D.gravityScale = 0;
			mRigidbody2D.drag = 0;
			Vector2 currentBalloonPosition = transform.position;
			Vector2 touchPosition = TouchControl.GetTouchPosition();
			float touchDistance = Vector2.Distance (touchPosition, currentBalloonPosition);
			float balloonDistance = GetDistanceBetweenParentAndPosition();
			if(balloonDistance < mDistanceJoint.distance)
			{
				DragBalloon(touchPosition, currentBalloonPosition);
			}
			else {
				SetVelocity(Vector2.zero);
				DragCharacter(touchDistance, currentBalloonPosition);
			}
		} else {
			mRigidbody2D.drag = 1;
			mRigidbody2D.gravityScale = -1;
			mCharacterPull.StopPulling();
		}
	}

	private void DragBalloon(Vector2 pTouchPosition, Vector2 pCurrentBalloonPosition)
	{
		//mCharacterPull.StopPulling();
		float xDistance = pTouchPosition.x - pCurrentBalloonPosition.x;
		float yDistance = pTouchPosition.y - pCurrentBalloonPosition.y;
		Vector2 velocity = new Vector2(xDistance, yDistance);
		SetVelocity(velocity);
	}

	private void DragCharacter(float pTouchDistance, Vector2 pCurrentBalloonPosition)
	{
		mCharacterPull.SetPullStrength(pTouchDistance - mDistanceJoint.distance);
		float mDirection = pCurrentBalloonPosition.x - mTack.transform.position.x;
		mCharacterPull.SetPullDirection(mDirection);
	}

	private void SetVelocity(Vector2 velocity)
	{
		mRigidbody2D.velocity = velocity;
	}

    private float GetDistanceBetweenParentAndPosition()
    {
		return Vector2.Distance(mTack.transform.position, transform.TransformPoint(mDistanceJoint.anchor));
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
		if (mCharacterPull != null && mCharacterPull.IsPulling ()) {
			isPulling = true;
		}
		return isPulling;
	}
}
