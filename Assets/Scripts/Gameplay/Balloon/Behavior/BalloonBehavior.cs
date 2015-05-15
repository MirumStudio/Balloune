using UnityEngine;
using System.Collections;

public abstract class BalloonBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
