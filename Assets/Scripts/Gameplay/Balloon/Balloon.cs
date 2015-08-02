/* -----      MIRUM STUDIO      -----
 * Copyright (c) 2015 All Rights Reserved.
 * 
 * This source is subject to a copyright license.
 * For more information, please see the 'LICENSE.txt', which is part of this source code package.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Balloon : MonoBehaviour {
	protected const int INFLATE_FACTOR = 5;

    const string POP_FX = "Particle/BalloonPopFX";


	private GameObject mBalloonObject = null;
    public EBalloonType Type { get; set; }
    public float Mass { get; set; }
	public SpriteRenderer SpriteRenderer { get; set; }

	public float m_MaxRopeDistance = 4f;

    private CircleCollider2D mCircleCollider = null;
    protected BalloonPhysics mPhysics = null;

    private List<BalloonBehavior> mBehaviors;
	private BalloonHolder mBalloonHolder;
	private int mBalloonIndex;

	public float GravityScale { get; set; }
	protected Vector3 mBaseScale;

	protected bool mIsDeflating = false;
	protected bool mIsInflating = false;

	protected Vector2 mCenterOfMass;

	virtual public void Init (EBalloonType pType) {
		mBalloonObject = transform.gameObject;
        mBehaviors = new List<BalloonBehavior>();
        mCircleCollider = GetComponent<CircleCollider2D>();
        mPhysics = GetComponent<BalloonPhysics>();
		SpriteRenderer = GetComponent<SpriteRenderer> ();
		AddBehavior<DefaultBehavior>();
		Type = pType;
		GravityScale = -1f;
		mBaseScale = transform.localScale;
		mCenterOfMass = new Vector2(0, SpriteRenderer.bounds.size.y/4);
	}

    protected void ChangeColor(Color pColor)
    {
        SpriteRenderer.color = pColor;
    }

	void Update () {
		Resize ();
		UpdateCenterOfMass ();
	}

    protected void AddBehavior<T>() where T : BalloonBehavior
    {
        var behavior = gameObject.AddComponent<T>();
        mBehaviors.Add(behavior);
    }

	public BalloonPhysics Physics
	{
		get { return mPhysics; }
	}

	public GameObject GameObject
    {
		get { return mBalloonObject; }
	}

	public CircleCollider2D CircleCollider
	{
		get { return mCircleCollider; }
	}

    public virtual void OnMove(float pDistance)
    {
        foreach(BalloonBehavior behavior in mBehaviors)
        {
            behavior.OnMove(pDistance);
        }
    }

    public virtual void OnPop()
    {
        foreach (BalloonBehavior behavior in mBehaviors)
        {
            behavior.OnPop();
        }

        var obj = PrefabFactory.Instantiate(POP_FX, GameObject.transform.position);
        Destroy(obj, 1.2f);

        if (BalloonHolder != null)
		{
            BalloonHolder.DestroyBalloon(this);
        }
        else
        {
            DestroyObject(GameObject);
        }
    }

	public void SetBalloonHolder(BalloonHolder pBalloonHolder)
	{
		mBalloonHolder = pBalloonHolder;
		mPhysics.SetBalloonHolder (mBalloonHolder);
	}

	public BalloonHolder BalloonHolder
	{
		get { return mBalloonHolder; }
	}

	public void SetBalloonIndex(int pBalloonIndex)
	{
		mBalloonIndex = pBalloonIndex;
		mPhysics.SetBalloonIndex (mBalloonIndex);
	}

	private void Resize()
	{
		if (mIsDeflating) {
			Deflate ();
		} else if (mIsInflating) {
			Inflate ();
		}
	}

	protected virtual void Deflate() {
		mIsInflating = false;
		mIsDeflating = true;
		if (transform.localScale.magnitude > (mBaseScale.magnitude / INFLATE_FACTOR)) {
			transform.localScale = transform.localScale * 0.999f;
		} else {
			mIsDeflating = false;
		}
	}

	public void InstantDeflate()
	{
		transform.localScale = transform.localScale / INFLATE_FACTOR;
	}

	private void Inflate()
	{
		mIsDeflating = false;
		mIsInflating = true;
		if (transform.localScale.magnitude < mBaseScale.magnitude) {
			transform.localScale = transform.localScale * 1.05f;
		} else if(transform.localScale.magnitude >= mBaseScale.magnitude){
			mIsInflating = false;
		}
	}

	public int BalloonIndex
	{
		get { return mBalloonIndex; }
	}

	public bool IsDeflating()
	{
		return mIsDeflating;
	}

	public void SetDeflate(bool pIsDeflating)
	{
		mIsDeflating = pIsDeflating;
		mIsInflating = !pIsDeflating;
	}

	public bool IsInflating()
	{
		return mIsInflating;
	}

	public void SetInflate(bool pIsInflating)
	{
		mIsInflating = pIsInflating;
		mIsDeflating = !mIsInflating;
	}

	public bool IsFullSize()
	{
		return transform.localScale.magnitude >= mBaseScale.magnitude;
	}

	protected virtual void UpdateCenterOfMass(){
		mCenterOfMass.y = SpriteRenderer.bounds.size.y/4;
		Physics.GetRigidBody().centerOfMass = mCenterOfMass;
	}
	
}
