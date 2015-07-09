/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;

public class GasPoint {
	
	public float GravityScale { get; set; }
	public Vector2 Position { get; set; }
	public float TotalAngle { get; set; }
	
	public Vector2 PreviousVector { get; set; }
	public Vector2 CurrentVector { get; set; }

	public GasPoint(Vector2 pPosition)
	{
		Position = pPosition;
		TotalAngle = 0f;
		PreviousVector = new Vector2 ();
		CurrentVector = new Vector2 ();
	}
	
	public void Reset()
	{
		TotalAngle = 0;
		PreviousVector = new Vector2 ();
		CurrentVector = new Vector2 ();
	}
}
