using UnityEngine;
using System.Collections;

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
    public Transform mParent = null;

    private LineRenderer mline = null;
    private Rigidbody2D mRigidbody2D = null;
    private SpringJoint2D mSpringJoint2D = null;

    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mSpringJoint2D = GetComponent<SpringJoint2D>();
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
    }

    private float GetDistanceBetweenParentAndPosition()
    {
        return Vector2.Distance(mParent.position, transform.position);
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

    private void AjustAngularDrag()
    {
        mRigidbody2D.angularDrag = m_AngularDrag;
    }

    private void UpdateLinePosition()
    {
        mline.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1));
        mline.SetPosition(1, new Vector3(mParent.position.x, mParent.position.y, -1));
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
}
