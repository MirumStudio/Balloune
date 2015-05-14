using UnityEngine;
using System.Collections;

public abstract class Balloon : MonoBehaviour {

    public EBalloonType Type { get; set; }
    public float Mass { get; set; }

    private CircleCollider2D mCircleCollider = null;
    private BalloonPhysics mPhysics = null;

	virtual public void Init () {
        mCircleCollider = GetComponent<CircleCollider2D>();
        mPhysics = GetComponent<BalloonPhysics>();
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

    public BalloonPhysics Physic
    {
        get { return mPhysics; }
    }
}
