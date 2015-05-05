using UnityEngine;

public class Rope : MonoBehaviour
{
	private int mRopeLength;

	private GameObject[] mRopeSegments;

	[SerializeField]
	private GameObject m_RopeSegmentPrefab;

	public Rope (int pRopeLength)
	{
		/*mRopeLength = pRopeLength;
		mRopeSegments = new GameObject[mRopeLength];

		for (int i = 0; i < mRopeLength; i++) {
			GameObject newSegment = new GameObject();
			Rigidbody2D newRigidBody = newSegment.AddComponent<Rigidbody2D>();
			HingeJoint2D newHinge = newSegment.AddComponent<HingeJoint2D>();
			LineRenderer newLineRenderer = newSegment.AddComponent<LineRenderer>();
			//SpriteRenderer newSprite = newSegment.AddComponent<SpriteRenderer>();
			newSegment.name = "hinge" + i;
			newRigidBody.gravityScale = -1;
			newRigidBody.drag = 1;
			newLineRenderer.material = (Material)Resources.Load("Assets/Ressources/Rope/ropeMaterial.mat");
			//newSprite.sortingOrder = 24;
			//newSprite.sprite = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Ressources/balloonRope"), new Rect(0,0,10,100), new Vector2(0,0));
			//newSprite.sprite = Resources.Load<Sprite>("balloonRope");
			//newSprite.sprite = (Sprite) Resources.LoadAssetAtPath("Assets/Ressources/balloonRope", typeof(Sprite));
			if (i > 0) {
				newHinge.connectedBody = mRopeSegments[i - 1].GetComponent<Rigidbody2D>();
				newHinge.connectedAnchor = new Vector2(0, 0.5f);
			}
			else {
				newHinge.connectedAnchor = new Vector2(0,0);
			}
			newHinge.anchor = new Vector2(0, -0.5f);
			mRopeSegments[i] = newSegment;
		}*/
	}

	public void createRope(int pRopeLength)
	{
		mRopeLength = pRopeLength;
		mRopeSegments = new GameObject[mRopeLength];
		
		for (int i = 0; i < mRopeLength; i++) {
			GameObject newSegment = Instantiate (m_RopeSegmentPrefab, new Vector2 (0, i), Quaternion.identity) as GameObject;
			LineRenderer segmentLineRenderer = newSegment.GetComponent<LineRenderer>();
			HingeJoint2D segmentHinge = newSegment.GetComponent<HingeJoint2D>();
			newSegment.name = "segment" + i;
			if(i > 0)
			{
				GameObject previousSegment = mRopeSegments[i - 1];
				//segmentLineRenderer.SetPosition (0, new Vector3(0, previousSegment.transform.position.y - 0.5f, 19));
				//segmentLineRenderer.SetPosition (1, new Vector3(0, previousSegment.transform.position.y + 0.5f, 19));
				segmentHinge.connectedBody = mRopeSegments[i - 1].GetComponent<Rigidbody2D>();
				segmentHinge.connectedAnchor = new Vector2(0, 0.5f);
			}
			else {
				segmentHinge.connectedAnchor = new Vector2(0,0);
			}

			mRopeSegments[i] = newSegment;
		}
	}

	public int GetLength()
	{
		return mRopeLength;
	}

	public GameObject GetStartOfRope()
	{
		return mRopeSegments[0];
	}

	public GameObject GetEndOfRope()
	{
		return mRopeSegments[mRopeLength - 1];
	}
}

