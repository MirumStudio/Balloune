using UnityEngine;
using System.Collections;
using Radix.Event;

public class MoveableObject : MonoBehaviour {

	private Rigidbody2D mRigidbody;
	private DistanceJoint2D mDistanceJoint;

	private bool mIsFlying = false;
	
	
	void Start () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		mDistanceJoint = GetComponent<DistanceJoint2D> ();
	}

	public bool IsFlying()
	{
		return mIsFlying;
	}

	public void SetIsFlying(bool pIsFlying)
	{
		mIsFlying = pIsFlying;
	}

	public DistanceJoint2D GetDistanceJoint()
	{
		return mDistanceJoint;
	}
}
