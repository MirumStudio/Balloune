/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;

public class RopeRenderer
{	
	private const int Z_AXIS = 3;
	private HingeJoint2D[] mRopeSegmentsHinges = null;
	
	private LineRenderer mLineRenderer = null;

	private int vertexCount = 2;
	private int vertexToDraw = -1;
	
	public RopeRenderer (LineRenderer pLineRenderer, GameObject[] pRopeSegments)
	{
		mLineRenderer = pLineRenderer;
		mRopeSegmentsHinges = new HingeJoint2D[pRopeSegments.Length];
		for (int i = 0; i < pRopeSegments.Length; i++) {
			mRopeSegmentsHinges[i] = pRopeSegments[i].GetComponent<HingeJoint2D>();
		}
		vertexCount = mRopeSegmentsHinges.Length + 2;
		mLineRenderer.SetVertexCount (vertexCount);
	}
	
	public void DrawRope(HingeJoint2D balloonJoint)
	{
		vertexToDraw = vertexCount - 1;
		DrawJoint (balloonJoint);
		for (int i = mRopeSegmentsHinges.Length - 1; i >= 0; i--) {
			DrawJoint (mRopeSegmentsHinges[i]);
		}
		DrawLastJoint (mRopeSegmentsHinges [0]);
	}

	private void DrawJoint(HingeJoint2D jointToDraw)
	{
		Vector2 positionToDraw = GetAnchorWorldSpace(jointToDraw);
		DrawPosition (positionToDraw);
	}

	private void DrawLastJoint(HingeJoint2D jointToDraw)
	{
		Vector2 positionToDraw = GetConnectedAnchorWorldSpace(jointToDraw, jointToDraw.connectedBody);
		DrawPosition (positionToDraw);
	}
	
	private void DrawPosition(Vector2 positionToDraw)
	{
		mLineRenderer.SetPosition (vertexToDraw, new Vector3 (positionToDraw.x, positionToDraw.y, Z_AXIS));
		vertexToDraw--;
	}
	
	private Vector2 GetAnchorWorldSpace(HingeJoint2D hingeJoint)
	{
		return hingeJoint.transform.TransformPoint (hingeJoint.anchor);
	}
	
	private Vector2 GetConnectedAnchorWorldSpace(HingeJoint2D hingeJoint, Rigidbody2D connectedBody)
	{
		return connectedBody.transform.TransformPoint (hingeJoint.connectedAnchor);
	}
}

