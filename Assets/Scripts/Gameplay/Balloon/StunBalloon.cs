using UnityEngine;
using System.Collections;

public class StunBalloon : Balloon {
	
	private const float STUN_BALLOON_DISTANCE = 2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.yellow);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxBalloonDistance = STUN_BALLOON_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
