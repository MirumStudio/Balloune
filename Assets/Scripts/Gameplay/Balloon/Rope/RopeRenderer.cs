using UnityEngine;

public class RopeRenderer
{	
	private const int Z_AXIS = 3;
	private HingeJoint2D[] mRopeSegmentsHinges = null;
	
	private LineRenderer mLineRenderer = null;

	private int mVertexCount = 2;
	private int mVertexToDraw = -1;

	private bool mIsAttached = true;
	
	public RopeRenderer (LineRenderer pLineRenderer, GameObject[] pRopeSegments)
	{
		mLineRenderer = pLineRenderer;
		mRopeSegmentsHinges = new HingeJoint2D[pRopeSegments.Length];
		for (int i = 0; i < pRopeSegments.Length; i++) {
			mRopeSegmentsHinges[i] = pRopeSegments[i].GetComponent<HingeJoint2D>();
		}
		mVertexCount = mRopeSegmentsHinges.Length + 2;
		mLineRenderer.SetVertexCount (mVertexCount);
	}
	
	public void DrawRope(HingeJoint2D balloonJoint)
	{
		mVertexToDraw = mVertexCount - 1;
		DrawJoint (balloonJoint);
		for (int i = mRopeSegmentsHinges.Length - 1; i >= 0; i--) {
			DrawJoint (mRopeSegmentsHinges[i]);
		}
		if (mIsAttached) {
			DrawLastJoint (mRopeSegmentsHinges [0]);
		}
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
		mLineRenderer.SetPosition (mVertexToDraw, new Vector3 (positionToDraw.x, positionToDraw.y, Z_AXIS));
		mVertexToDraw--;
	}
	
	private Vector2 GetAnchorWorldSpace(HingeJoint2D hingeJoint)
	{
		return hingeJoint.transform.TransformPoint (hingeJoint.anchor);
	}
	
	private Vector2 GetConnectedAnchorWorldSpace(HingeJoint2D hingeJoint, Rigidbody2D connectedBody)
	{
		return connectedBody.transform.TransformPoint (hingeJoint.connectedAnchor);
	}

	public void Detach()
	{
		mIsAttached = false;
		Debug.Log (mIsAttached);
	}
}

