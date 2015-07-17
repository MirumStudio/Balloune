/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using Radix.Event;
using UnityEngine;
using System.Collections.Generic;

public class GasSource : MonoBehaviour {

	[SerializeField]
	private EBalloonType m_GasType = EBalloonType.TOXIC;

    [SerializeField]
    private float m_MinimumDistance = 4.0f;

	EdgeCollider2D mEdgeCollider;
	Vector2 mEdgeVector;

    float timer = 0f;

	List<GasPoint> mGasPoints = new List<GasPoint>();

	void Start()
	{
		mEdgeCollider = GetComponent<EdgeCollider2D> ();
		UpdateGasPoints ();
	}

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.touchCount > 0 && timer > 0.1)
        {
            timer = 0;
            VerifyCircle(Camera.main.ScreenToWorldPoint(Input.touches[0].position));
        }
		for (int i = 0; i < mGasPoints.Count; i++) {
			if (mGasPoints[i].TotalAngle != 0 && Input.touchCount > 0)
			{
				if(mGasPoints[i].TotalAngle > 330 || mGasPoints[i].TotalAngle < -330)
				{
					EventService.DispatchEvent(EGameEvent.INFLATE_BALLOON, m_GasType);
					resetAngle ();
				}
			}
		}
		
		if (Input.touchCount == 0) {
			resetAngle ();
		}
    }

	void FixedUpdate()
	{
		UpdateGasPoints ();
	}

    void VerifyCircle(Vector2 pos)
    {
		for (int i = 0; i < mGasPoints.Count; i++) {
			Vector2 previousVector = mGasPoints[i].PreviousVector;
			Vector2 currentVector = mGasPoints[i].CurrentVector;
			previousVector = new Vector2(currentVector.x, currentVector.y);
			currentVector = new Vector2(pos.x - mGasPoints[i].Position.x, pos.y - mGasPoints[i].Position.y);
			if (currentVector.magnitude <= m_MinimumDistance)
			{
				var constants_value = Mathf.Min(1, Vector2.Dot(previousVector, currentVector) / (previousVector.magnitude * currentVector.magnitude));
				
				if (!float.IsNaN(constants_value))
				{
					var delta_angle = Mathf.Acos(constants_value);
					
					var direction = CrossProduct(previousVector, currentVector) > 0 ? 1 : -1;
					
					mGasPoints[i].TotalAngle = ((Mathf.Rad2Deg * delta_angle) * direction) + mGasPoints[i].TotalAngle;
				}
			}
			mGasPoints[i].PreviousVector = previousVector;
			mGasPoints[i].CurrentVector = currentVector;
		}
    }

	void resetAngle() {
		for (int i = 0; i < mGasPoints.Count; i++) {
			mGasPoints[i].Reset ();
		}
	}
	
	float CrossProduct(Vector2 v1, Vector2 v2)
    {
        return (v1.x * v2.y) - (v1.y * v2.x);
    }

	private void UpdateGasPoints()
	{
		mGasPoints.Add (new GasPoint (GetColliderMaxBound()));
		mGasPoints.Add (new GasPoint (transform.position));
		mGasPoints.Add (new GasPoint (GetColliderMinBound()));
	}

	Vector2 GetColliderMaxBound()
	{
		Vector2 maxBoundPosition = transform.position;
		maxBoundPosition.x = maxBoundPosition.x + mEdgeCollider.bounds.extents.x;
		maxBoundPosition.y = maxBoundPosition.y + mEdgeCollider.bounds.extents.y;
		return maxBoundPosition;
	}

	Vector2 GetColliderMinBound()
	{
		Vector2 minBoundPosition = transform.position;
		minBoundPosition.x = minBoundPosition.x - mEdgeCollider.bounds.extents.x;
		minBoundPosition.y = minBoundPosition.y - mEdgeCollider.bounds.extents.y;
		return minBoundPosition;
	}
}
