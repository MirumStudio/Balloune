using UnityEngine;

public class Rope : Component
{
	private int ropeLength;

	private RopeSegment[] ropeSegments;

	public Rope (int pRopeLength)
	{
		ropeLength = pRopeLength;
		ropeSegments = new RopeSegment[ropeLength];

		for (int i = 0; i < ropeLength; i++) {
			RopeSegment newSegment = new RopeSegment();
			HingeJoint2D newHinge = new HingeJoint2D ();
			newSegment.SetHingeJoint(newHinge);
			if (i > 0) {
				newSegment.GetHingeJoint().connectedBody = ropeSegments[i - 1].GetRigidBody();
			}
			ropeSegments[i] = newSegment;
		}
	}

	public int GetLength()
	{
		return ropeLength;
	}

	public RopeSegment GetStartOfRope()
	{
		return ropeSegments[0];
	}

	public RopeSegment GetEndOfRope()
	{
		return ropeSegments[ropeLength - 1];
	}
}

