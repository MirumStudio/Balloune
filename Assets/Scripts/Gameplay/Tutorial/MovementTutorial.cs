using UnityEngine;
using System.Collections;
using Radix.Event;

public class MovementTutorial : MonoBehaviour {

	[SerializeField]
	private GameObject m_TutoPrefab;

	private GameObject m_ObjectToAttachTuto = null;
	private GameObject mTuto = null;
	private bool mTutoGiven = false;
	// Use this for initialization
	void Start () {
		EventService.Register<BalloonDelegate> (EGameEvent.PICKUP_BALLOON, OnStopTutoEvent);
		m_ObjectToAttachTuto = this.GetComponentInChildren<Balloon> ().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!mTutoGiven) 
		{
			GiveTutoAnimation();
		}
	}

	private void GiveTutoAnimation()
	{
		mTutoGiven = true;

		mTuto = PrefabFactory.Instantiate(m_TutoPrefab, m_ObjectToAttachTuto);
		//mTuto.transform.Translate (m_ObjectToAttachTuto.transform.position);
	}

	private void OnStopTutoEvent(Balloon pBalloon)
	{
		DestroyObject (mTuto);
		//mTuto.GetComponent<Animator> ().CrossFade ("New Animation3", 0f);
	}
}
