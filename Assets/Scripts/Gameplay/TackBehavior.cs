using UnityEngine;
using System.Collections;
using Radix.Event;
using System;

public class TackBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventListener.Register(EGameEvent.ATTEMPT_ATTACH_BALLOON, OnAttemptAttachBalloon);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnAttemptAttachBalloon(Enum pEvent, object pBalloon, object pPosition)
	{
		Collider2D[] touchedColliders = Physics2D.OverlapCircleAll ((Vector2)pPosition,  1f);
		Collider2D thisCollider = transform.parent.GetComponent<Collider2D> ();
		for(int i = 0; i < touchedColliders.Length; i++)
		{
			if(touchedColliders[i] == thisCollider)
			{
				EventService.DipatchEvent(EGameEvent.ATTACH_BALLOON, pBalloon, this.gameObject);
				break;
			}
		}
	}
}
