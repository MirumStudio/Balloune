/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BreakableObject : MonoBehaviour {
	
	private Rigidbody2D mRigidBody;

	[SerializeField]
	private float m_BreakMass = 4f;

	void Start () {
		mRigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.rigidbody != null && (collision.relativeVelocity.magnitude > 4 && collision.rigidbody.mass >= m_BreakMass)) {
			Break (collision.relativeVelocity);
		}
	}

	private void Break(Vector2 pRelativeVelocity)
	{
		Transform parent = transform.parent;
		int numberOfParts = parent.childCount;
		for (int i = 0; i < numberOfParts; i++) {
			Transform child = parent.GetChild (i);
			child.gameObject.layer = 12;
			Rigidbody2D brokenPartRigidbody = child.GetComponent<Rigidbody2D>();
			brokenPartRigidbody.isKinematic = false;
			brokenPartRigidbody.velocity = GetFallingVelocity(pRelativeVelocity);
			child.GetComponent<BreakableObject>().enabled = false;
		}
	}

	private Vector2 GetFallingVelocity(Vector2 pRelativeVelocity)
	{
		return pRelativeVelocity * -1;
	}
}
