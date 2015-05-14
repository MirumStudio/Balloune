using UnityEngine;
using System.Collections;

public abstract class Balloon : MonoBehaviour {

    public EBalloonType Type { get; set; }
    public float Mass { get; set; }

    private CircleCollider2D mCircleCollider = null;
    private BalloonBehavior mBehavior = null;

	virtual public void Init () {
        mCircleCollider = GetComponent<CircleCollider2D>();
        mBehavior = GetComponent<BalloonBehavior>();
	}

    protected void ChangeColor(Color pColor)
    {
        GetComponent<SpriteRenderer>().color = pColor;
    }


	void Update () {
	
	}

    public CircleCollider2D CircleCollider
    {
        get { return mCircleCollider; }
    }

    public BalloonBehavior Behavior
    {
        get { return mBehavior; }
    }
}
