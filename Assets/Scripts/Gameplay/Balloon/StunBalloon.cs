using UnityEngine;
using System.Collections;

public class StunBalloon : Balloon {
	
	public const float STUN_ROPE_DISTANCE = 2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.yellow);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxRopeDistance = STUN_ROPE_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
