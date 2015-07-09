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
		EventService.Register<BalloonDelegate>(m_StoppingEvent, OnStoppingEvent);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_ObjectToAttachTuto == null) 
		{
			try 
			{
				if(m_ObjectToAttachTuto != null)
				{
					m_ObjectToAttachTuto = this.GetComponentInChildren<Balloon> ().gameObject;
				}
			} 
			catch (UnityException ex) 
			{

			}
		} else if (!mTutoGiven) 
		{
			GiveTutoAnimation();
		}
	}

	private void GiveTutoAnimation()
	{
		mTutoGiven = true;

		mTuto = PrefabFactory.Instantiate(m_TutoPrefab, m_ObjectToAttachTuto);

	}

	private void OnStoppingEvent(Balloon pBalloon)
	{
		DestroyObject (mTuto);
		//mTuto.GetComponent<Animator> ().CrossFade ("New Animation3", 0f);
	}
}
