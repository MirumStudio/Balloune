using UnityEngine;

public class Rope : Component
{
	private int ropeLength;

	private GameObject[] ropeSegments;

	public Rope (int pRopeLength)
	{
		ropeLength = pRopeLength;
		ropeSegments = new GameObject[ropeLength];

		for (int i = 0; i < ropeLength; i++) {
			GameObject newSegment = new GameObject();
			Rigidbody2D newRigidBody = newSegment.AddComponent<Rigidbody2D>();
			HingeJoint2D newHinge = newSegment.AddComponent<HingeJoint2D>();
			SpriteRenderer newSprite = newSegment.AddComponent<SpriteRenderer>();
			newSegment.name = "hinge" + i;
			newRigidBody.gravityScale = -1;
			newRigidBody.drag = 1;
			newSprite.sortingOrder = 24;
			//newSprite.sprite = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Ressources/balloonRope"), new Rect(0,0,10,100), new Vector2(0,0));
			newSprite.sprite = Resources.Load<UnityEngine.Sprite>("Assets/Ressources/balloonRope.jpg");
			if (i > 0) {
				newHinge.connectedBody = ropeSegments[i - 1].GetComponent<Rigidbody2D>();
			}
			else {
				newHinge.connectedAnchor = new Vector2(0,0);
			}
			newHinge.anchor = new Vector2(0, -1);
			ropeSegments[i] = newSegment;
		}
	}

	public int GetLength()
	{
		return ropeLength;
	}

	public GameObject GetStartOfRope()
	{
		return ropeSegments[0];
	}

	public GameObject GetEndOfRope()
	{
		return ropeSegments[ropeLength - 1];
	}
}

