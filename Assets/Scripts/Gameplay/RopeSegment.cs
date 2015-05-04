using UnityEngine;

public class RopeSegment
{
	GameObject gameObject = null;

	HingeJoint2D hingeJoint2D = null;
	Rigidbody2D rigidBody2D = null;

	public RopeSegment ()
	{
		gameObject = new GameObject ();
	}

	public void SetHingeJoint(HingeJoint2D pHingeToSet)
	{
		hingeJoint2D = pHingeToSet;
		hingeJoint2D = gameObject.AddComponent<HingeJoint2D> ();
	}

	public HingeJoint2D GetHingeJoint()
	{
		return hingeJoint2D;
	}

	public void SetRigidBody(Rigidbody2D pRigidBodyToSet)
	{
		rigidBody2D = pRigidBodyToSet;
		rigidBody2D = gameObject.AddComponent<Rigidbody2D> ();
	}
	
	public Rigidbody2D GetRigidBody()
	{
		return rigidBody2D;
	}
}

