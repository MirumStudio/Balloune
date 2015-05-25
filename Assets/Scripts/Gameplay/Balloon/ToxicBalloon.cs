using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {
	
	public const float TOXIC_ROPE_DISTANCE = 2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.green);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxRopeDistance = TOXIC_ROPE_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
