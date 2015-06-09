/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;

public abstract class BalloonBehavior : MonoBehaviour {

	// Use this for initialization
	protected Balloon mBalloon = null;

	protected virtual void Start () {
		mBalloon = GetComponent<Balloon> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OnMove(float pDistance)
    {

    }

    public virtual void OnPop()
	{

    }
}
