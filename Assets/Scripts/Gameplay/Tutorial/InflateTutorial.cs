using UnityEngine;
using System.Collections;
using Radix.Event;

public class InflateTutorial : MonoBehaviour {
	
	[SerializeField]
	private GameObject m_TutoPrefab;
	
	[SerializeField]
	private GameObject m_ObjectToAttachTuto = null;
	private GameObject mTuto = null;
	private bool mTutoGiven = false;
	// Use this for initialization
	void Start () {
		EventService.Register<BalloonTypeDelegate> (EGameEvent.INFLATE_BALLOON, OnStopTutoEvent);
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
	
	private void OnStopTutoEvent(EBalloonType pType)
	{
		DestroyObject (mTuto);
		//mTuto.GetComponent<Animator> ().CrossFade ("New Animation3", 0f);
	}
}
