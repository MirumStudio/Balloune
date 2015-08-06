/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;
using Radix.Event;

public class MoveableObject : MonoBehaviour {

	private Rigidbody2D mRigidbody;
	private DistanceJoint2D mDistanceJoint;

	private bool mIsFlying = false;

	private float mBaseMass = 0f;
	[SerializeField]
	private float m_FlyingMass = 0f;

	protected virtual void Start () {
		mRigidbody = GetComponent<Rigidbody2D> ();
		mDistanceJoint = GetComponent<DistanceJoint2D> ();
		mBaseMass = mRigidbody.mass;
	}

	public bool IsFlying()
	{
		return mIsFlying;
	}

	public void SetIsFlying(bool pIsFlying)
	{
		mIsFlying = pIsFlying;
		UpdateMass ();
	}

	public DistanceJoint2D GetDistanceJoint()
	{
		return mDistanceJoint;
	}

	private void UpdateMass()
	{
		if (mIsFlying) {
			mRigidbody.mass = m_FlyingMass;
		} else {
			mRigidbody.mass = mBaseMass;
		}
	}
}
