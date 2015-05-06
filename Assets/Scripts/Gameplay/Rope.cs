using UnityEngine;

public class Rope : MonoBehaviour
{
	private float mRopeLength;
	private int mNumberOfHinges;
	private float mLengthOfEachSegment;

	private GameObject[] mRopeSegments;

	[SerializeField]
	private GameObject m_RopeSegmentPrefab = null;

	public Rope (int pRopeLength)
	{

	}

	public void createRope(float pRopeLength, int pNumberOfHinges)
	{
		mRopeLength = pRopeLength;
		mNumberOfHinges = pNumberOfHinges;
		mRopeSegments = new GameObject[mNumberOfHinges];
		mLengthOfEachSegment = GetLengthOfEachSegment();
		CreateRopeSegments ();
		AttachRopeSegments ();
	}

	private void CreateRopeSegments()
	{
		for (int i = 0; i < mNumberOfHinges; i++) {
			GameObject newSegment = Instantiate (m_RopeSegmentPrefab, new Vector2 (0, i), Quaternion.identity) as GameObject;
			LineRenderer segmentLineRenderer = newSegment.GetComponent<LineRenderer> ();
			segmentLineRenderer.SetPosition (1, new Vector3 (0, mLengthOfEachSegment, -1));
			newSegment.name = "segment" + i;
			
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
	
	public float GetLengthOfEachSegment()
	{
		return (mRopeLength / mNumberOfHinges) - 0.5f;
	}

	public GameObject GetStartOfRope()
	{
		return mRopeSegments[0];
	}

	public GameObject GetEndOfRope()
	{
		return mRopeSegments[mNumberOfHinges - 1];
	}
}

