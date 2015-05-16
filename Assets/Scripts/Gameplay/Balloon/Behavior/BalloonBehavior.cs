using UnityEngine;
using System.Collections;

public abstract class BalloonBehavior : MonoBehaviour {

	// Use this for initialization
	protected Balloon mBalloon = null;

	protected virtual void Start () {
		mBalloon = GetComponent<Balloon> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OnMove(float pDistance)
    {

    }

    public virtual void OnPop()
    {

    }
}
