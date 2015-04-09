using UnityEngine;
using System;
using System.Collections;
using Radix.Event;
using Radix.Error;

public class BalloonBehavior : MonoBehaviour
{
    [SerializeField]
    private float m_MaxDistance = 4.00f;

    [SerializeField]
    private float m_AngularDrag = 1f;

    [SerializeField]
    private float m_MaxVelocityX = 3.0f;

    [SerializeField]
    private float m_MinVelocityY = -1.0f;

    [SerializeField]
    public Transform m_Parent = null;

	public int balloonIndex = -1;

    private LineRenderer mline = null;
    private Rigidbody2D mRigidbody2D = null;
    private SpringJoint2D mSpringJoint2D = null;
	private BalloonHolder balloonHolder = null;

	[SerializeField]
	private float m_MaximumInvulnerableTime = 2f;
	private bool mIsInvulnerable = false;
	private float mInvulnerableTime = 0f;

    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
		mSpringJoint2D = GetComponent<SpringJoint2D>();
		balloonHolder = (BalloonHolder) this.mSpringJoint2D.connectedBody.GetComponent<BalloonHolder>();
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
        Ajustvelocity();
        AjustAngularDrag();
        UpdateLinePosition();
        UpdateSpringJoint(GetDistanceBetweenParentAndPosition());
		CheckInvulnerability ();
    }

    private float GetDistanceBetweenParentAndPosition()
    {
        return Vector2.Distance(m_Parent.position, transform.position);
    }

    private void UpdateSpringJoint(float aDistance)
    {
        if (aDistance >= m_MaxDistance)
        {
            mSpringJoint2D.enabled = true;
        }
        else if (aDistance < m_MaxDistance && mSpringJoint2D.enabled)
        {
            mSpringJoint2D.enabled = false;
        }
    }

	private void CheckInvulnerability() {
		if (mIsInvulnerable) {
			mInvulnerableTime += Time.deltaTime;
			if (mInvulnerableTime >= m_MaximumInvulnerableTime) {
				mIsInvulnerable = false;
				mInvulnerableTime = 0;
			}
		}
	}

    private void AjustAngularDrag()
    {
        mRigidbody2D.angularDrag = m_AngularDrag;
    }

    private void UpdateLinePosition()
    {
        mline.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1));
        mline.SetPosition(1, new Vector3(m_Parent.position.x, m_Parent.position.y, -1));
    }

    private void Ajustvelocity()
    {
        mRigidbody2D.velocity = new Vector2(AjustVelocityX(), AjustVelocityY());
    }

    private float AjustVelocityX()
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

    private float AjustVelocityY()
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
}
