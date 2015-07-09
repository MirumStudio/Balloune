using UnityEngine;
using System.Collections;
using Radix.Event;

public class Door : MonoBehaviour {
	
	[SerializeField]
	public Transform m_GearBox;

	private SliderJoint2D mSliderJoint;

	
	void Start () {
		mSliderJoint = GetComponent<SliderJoint2D> ();
		EventService.Register<Vector2Delegate>(EGameEvent.STUN_BALLOON_POP, OnStunBalloonPop);
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
		return distance <= 3;
	}
	
	private void RaiseDoor()
	{
		mSliderJoint.useMotor = true;
	}
}
