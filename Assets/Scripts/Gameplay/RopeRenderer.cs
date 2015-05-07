using UnityEngine;

public class RopeRenderer
{	
	private HingeJoint2D[] mRopeSegmentsHinges = null;
	
	private LineRenderer mLinerenderer = null;
	
	int vertexToDraw = -1;
	
	public RopeRenderer (GameObject[] pRopeSegments)
	{
		mRopeSegmentsHinges = new HingeJoint2D[pRopeSegments.Length];
		for (int i = 0; i < pRopeSegments.Length; i++) {
			mRopeSegmentsHinges[i] = pRopeSegments[i].GetComponent<HingeJoint2D>();
		}
	}
	
	public void DrawRope(LineRenderer pLineRenderer, HingeJoint2D balloonJoint)
	{
		mLinerenderer = pLineRenderer;
		int vertexCount = mRopeSegmentsHinges.Length + 2;
		mLinerenderer.SetVertexCount (vertexCount);
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
		mLinerenderer.SetPosition (vertexToDraw, new Vector3 (positionToDraw.x, positionToDraw.y, -1));
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

