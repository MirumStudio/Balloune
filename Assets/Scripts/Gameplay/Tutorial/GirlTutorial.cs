using UnityEngine;
using System.Collections;
using Radix.Event;

public class GirlTutorial : MonoBehaviour {

	[SerializeField]
	private GameObject m_TutoPrefab;

	[SerializeField]
	private EGameEvent m_StoppingEvent;

	[SerializeField]
	private GameObject m_ObjectToAttachTuto = null;
	private GameObject mTuto = null;
	private bool mTutoGiven = false;
	// Use this for initialization
	void Start () {
		if (m_StoppingEvent == EGameEvent.PICKUP_BALLOON) {
			EventService.Register<BalloonDelegate> (m_StoppingEvent, OnStopTouchTutoEvent);
		} else if (m_StoppingEvent == EGameEvent.INFLATE_BALLOON) {
			EventService.Register<BalloonTypeDelegate> (m_StoppingEvent, OnStopInflateTutoEvent);
		}
		try 
		{
			if(m_ObjectToAttachTuto == null)
			{
				m_ObjectToAttachTuto = this.GetComponentInChildren<Balloon> ().gameObject;
			}
		} 
		catch (UnityException ex) 
		{
			
		}
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

		mTuto = PrefabFactory.Instantiate(m_TutoPrefab, m_ObjectToAttachTuto.transform.position);
	}

	private void OnStopTouchTutoEvent(Balloon pBalloon)
	{
		DestroyObject (mTuto);
		//mTuto.GetComponent<Animator> ().CrossFade ("New Animation3", 0f);
	}

	private void OnStopInflateTutoEvent(EBalloonType pType)
	{
		DestroyObject (mTuto);
		//mTuto.GetComponent<Animator> ().CrossFade ("New Animation3", 0f);
	}
}
