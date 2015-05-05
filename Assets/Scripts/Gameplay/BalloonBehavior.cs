using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.Error;

public class BalloonBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_MaxVelocityX = 3.0f;

    [SerializeField]
    private float m_MinVelocityY = -1.0f;

    [SerializeField]
    public Transform m_Parent = null;

	public int balloonIndex = -1;

    private LineRenderer mline = null;
    private Rigidbody2D mRigidbody2D = null;
	//private SpringJoint2D mSpringJoint2D = null;
	private DistanceJoint2D mDistanceJoint2D = null;
	private BalloonHolder balloonHolder = null;
	private Rope rope = null;

	[SerializeField]
	private float m_MaximumInvulnerableTime = 2f;
	private bool mIsInvulnerable = false;
	private float mInvulnerableTime = 0f;

    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
		//mSpringJoint2D = GetComponent<SpringJoint2D>();
		mDistanceJoint2D = GetComponent<DistanceJoint2D>();
		//balloonHolder = (BalloonHolder) this.mSpringJoint2D.connectedBody.GetComponent<BalloonHolder>();
		//balloonHolder = (BalloonHolder) this.mDistanceJoint2D.connectedBody.GetComponent<BalloonHolder>();
		rope = GetComponent<Rope> ();
		balloonHolder = (BalloonHolder)rope.GetStartOfRope().GetComponent<HingeJoint2D>().connectedBody.GetComponent<BalloonHolder>();
		EventListener.Register(EGameEvent.HAZARDOUS_COLLISION, OnHazardousCollision);
        InitLineRenderer();

        //Testing purpose
        /*var renderer = GetComponent<SpriteRenderer>();
        mline.sortingLayerID = renderer.sortingLayerID;
        mline.sortingLayerName = renderer.sortingLayerName;
        mline.sortingOrder = renderer.sortingOrder;*/

    }

    private void InitLineRenderer()
    {
        mline = GetComponent<LineRenderer>();
        UpdateLinePosition();
    }

    private void Update()
    {
		CheckIfInvulnerable ();
    }

	private void FixedUpdate()
	{
		Adjustvelocity();
		//UpdateLinePosition();
	}

    private float GetDistanceBetweenParentAndPosition()
    {
		return Vector2.Distance(m_Parent.position, mRigidbody2D.position);
        //return Vector2.Distance(m_Parent.position, transform.position);
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

    private void UpdateLinePosition()
    {
		//UpdateLineDistance ();
        mline.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1));
        mline.SetPosition(1, new Vector3(m_Parent.position.x, m_Parent.position.y, -1));
    }

	private void UpdateLineDistance()
	{
		float maxDistance = mDistanceJoint2D.distance;
		//float maxDistance = mSpringJoint2D.distance;
		float currentDistance = GetDistanceBetweenParentAndPosition ();
		//Debug.Log ("CurrentDistance : " + currentDistance);
		bool fixRequired = false;
		if (currentDistance > maxDistance) {
			Vector2 balloonPosition = new Vector2(mRigidbody2D.position.x, mRigidbody2D.position.y);
			//Debug.Log ("currentTackPosition : (" + m_Parent.position.x + "," + m_Parent.position.y +")");
			//Debug.Log ("currentBalloonPosition : (" + balloonPosition.x + "," + balloonPosition.y +")");

			double distance = Vector2.Distance (balloonPosition, m_Parent.position);
			float distanceRatio = (float) (-maxDistance / distance);

			float distanceX = m_Parent.position.x - balloonPosition.x;
			float newX = distanceX * distanceRatio + m_Parent.position.x;

			float distanceY =  m_Parent.position.y - balloonPosition.y;
			float newY = distanceY * distanceRatio + m_Parent.position.y;

			//Debug.Log ("New Coordinates : (" + newX + "," + newY +")");
			mRigidbody2D.MovePosition(new Vector2(newX, newY));
			fixRequired = true;
		}
		//Debug.Log ("fixRequired : " + fixRequired);
	}

    private void Adjustvelocity()
    {
        mRigidbody2D.velocity = new Vector2(AdjustVelocityX(), AdjustVelocityY());
    }

    private float AdjustVelocityX()
    {
        float x = mRigidbody2D.velocity.x;

        if (x > 0)
        {
            x = Mathf.Min(x, m_MaxVelocityX);
        }
        else if (x < 0)
        {
            x = Mathf.Max(x, -m_MaxVelocityX);
        }

        return x;
    }

    private float AdjustVelocityY()
    {
        return Mathf.Max(m_MinVelocityY, mRigidbody2D.velocity.y);
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
			popBalloon();
		}
		checkIfGameOver();
	}

	private void popBalloon(){
		//TODO balloon explosion animation and sound effect
		balloonHolder.destroyBalloon(balloonIndex);
	}

	private void checkIfGameOver() {
		if (balloonHolder.countBalloons() <= 0)
		{
			EventService.DipatchEvent(EGameEvent.GAME_OVER, null);
		}
	}
	
	public void setInvulnerable(bool pInvulnerable) {
		this.mIsInvulnerable = pInvulnerable;
	}

	public Rope GetRope()
	{
		return rope;
	}

	public void SetRope(Rope pRope)
	{
		rope = pRope;
	}
}
