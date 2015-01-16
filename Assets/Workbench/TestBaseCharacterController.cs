using UnityEngine;
using System.Collections;
using System;

public abstract class TestBaseCharacterController : MonoBehaviour {

    public float moveForce = 350f;
    public float maxSpeed = 3f;
    bool jump = false;
    public float jumpForce = 1000f;	
      public Transform groundCheck;
      bool mIsGrounded;
	void Start () {
        groundCheck = transform.Find("GroundCheck");
	}
	
	void Update () {

        mIsGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.Log(mIsGrounded);
        if (Input.GetKey(KeyCode.Space) && mIsGrounded)
        {
            jump = true;
        }
	}

    void FixedUpdate()
    {
        float h = GetHorizontalAxisValue();

        if (h * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
        }

        if (jump)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Math.Min(rigidbody2D.velocity.y, 5.6f));
            jump = false;
        }
    }

    protected abstract float GetHorizontalAxisValue();
}
