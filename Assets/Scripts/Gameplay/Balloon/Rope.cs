using UnityEngine;

public class Rope : MonoBehaviour
{
	private float mRopeLength;
	private float mLengthOfEachSegment;

	private GameObject[] mRopeSegments;

	[SerializeField]
	private GameObject m_RopeSegmentPrefab = null;

	[SerializeField]
	private int m_NumberOfHinges = 15;

	private RopeRenderer mRopeRenderer = null;

	public Rope (){}

	public void createRope(float pRopeLength)
	{
		mRopeLength = pRopeLength;
		mRopeSegments = new GameObject[m_NumberOfHinges];
		mLengthOfEachSegment = GetLengthOfEachSegment();
		CreateRopeSegments ();
		AttachRopeSegments ();
		mRopeRenderer = new RopeRenderer (mRopeSegments);
	}

	private void CreateRopeSegments()
	{
		for (int i = 0; i < m_NumberOfHinges; i++) {
			GameObject newSegment = PrefabFactory.Instantiate (m_RopeSegmentPrefab, new Vector2 (0, i));
			newSegment.name = "segment" + i;
			newSegment.transform.parent = this.transform;
			mRopeSegments [i] = newSegment;
		}
	}

	private void AttachRopeSegments()
	{
		for (int i = 0; i < mRopeSegments.Length; i++) {
			HingeJoint2D segmentHinge = mRopeSegments[i].GetComponent<HingeJoint2D> ();
			if(i > 0)
			{
				GameObject previousSegment = mRopeSegments[i - 1];
				segmentHinge.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
				segmentHinge.connectedAnchor = new Vector2(0, mLengthOfEachSegment);
			}
			else {
				segmentHinge.connectedAnchor = new Vector2(0,0);
			}
		}
	}

	public void DrawRope(LineRenderer pLineRenderer, HingeJoint2D balloonJoint)
	{
		mRopeRenderer.DrawRope (pLineRenderer, balloonJoint);
	}

	public float GetLengthOfEachSegment()
	{
		return (mRopeLength / m_NumberOfHinges) - 0.5f;
	}

	public GameObject GetStartOfRope()
	{
		return mRopeSegments[0];
	}

	public GameObject GetEndOfRope()
	{
		return mRopeSegments[m_NumberOfHinges - 1];
	}
}

