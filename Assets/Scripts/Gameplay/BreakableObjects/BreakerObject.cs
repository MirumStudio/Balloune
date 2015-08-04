/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
public class BreakerObject : MonoBehaviour {

	private Rigidbody2D mRigidBody;

	// Use this for initialization
	void Start () {
		mRigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
	
	}
}
