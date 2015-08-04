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
	private GameObject m_BrokenObject;

	[SerializeField]
	private float m_BreakMass = 4f;
	// Use this for initialization

	void Start () {
		mRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.relativeVelocity.magnitude > 4 && collision.rigidbody.mass >= m_BreakMass) {
			Break ();
		}
	}

	private void Break()
	{
		PrefabFactory.Instantiate (m_BrokenObject, transform.position);
		PrefabFactory.Instantiate (m_BrokenObject, transform.position);
		GameObject.Destroy (this.gameObject);
	}
}
