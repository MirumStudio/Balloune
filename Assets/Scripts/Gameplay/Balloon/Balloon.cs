using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Balloon : MonoBehaviour {

    public EBalloonType Type { get; set; }
    public float Mass { get; set; }

    private CircleCollider2D mCircleCollider = null;
    private BalloonPhysics mPhysics = null;

    private List<BalloonBahavior> mBehaviors;

	virtual public void Init () {
        mBehaviors = new List<BalloonBahavior>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mPhysics = GetComponent<BalloonPhysics>();
        AddBehavior<DefaultBehavior>();
	}

    protected void ChangeColor(Color pColor)
    {
        GetComponent<SpriteRenderer>().color = pColor;
    }

	void Update () {
	
	}

    protected void AddBehavior<T>() where T : BalloonBahavior
    {
        var behavior = gameObject.AddComponent<T>();
        mBehaviors.Add(behavior);
    }

    public CircleCollider2D CircleCollider
    {
        get { return mCircleCollider; }
    }

    public BalloonPhysics Physic
    {
        get { return mPhysics; }
    }

    public virtual void OnMove(float pDistance)
    {
        foreach(BalloonBahavior behavior in mBehaviors)
        {
            behavior.OnMove(pDistance);
        }
    }
}
