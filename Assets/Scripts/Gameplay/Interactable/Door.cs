using UnityEngine;
using System.Collections;
using Radix.Event;

public class Door : MonoBehaviour {

	private const int AREA_OF_EFFECT = 3;

	[SerializeField]
	public Transform m_GearBox;

	[SerializeField]
	private GameObject m_LeverObject;
	private Lever mLever;

	private bool mIsTriggered = false;

	private SliderJoint2D mSliderJoint;

	
	void Start () {
		mSliderJoint = GetComponent<SliderJoint2D> ();
		EventService.Register<Vector2Delegate>(EGameEvent.STUN_BALLOON_POP, OnStunBalloonPop);
		if (m_LeverObject != null) {
			mLever = m_LeverObject.GetComponent<Lever> ();
		}
	}

	private void Update()
	{
		if (m_LeverObject != null && mIsTriggered == false) {
			Trigger ();
		}
	}

	private void Trigger()
	{
		if (mLever.IsTriggered ()) {
			RaiseDoor ();
			mIsTriggered = true;
		}
	}

	private void OnStunBalloonPop(Vector2 pPos)
	{
		if(IsNearGearBox(pPos))
		{
			RaiseDoor();
		}
	}
	
	private bool IsNearGearBox(Vector2 pPos)
	{
		float distance = Vector2.Distance(m_GearBox.position, pPos);
		return distance <= AREA_OF_EFFECT;
	}
	
	private void RaiseDoor()
	{
		mSliderJoint.useMotor = true;
	}
}
