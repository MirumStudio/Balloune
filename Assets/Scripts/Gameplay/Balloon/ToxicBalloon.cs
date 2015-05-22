using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {
	
	private const float TOXIC_BALLOON_DISTANCE = 2f;

	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
		ChangeColor(Color.green);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		AddBehavior<TriggerableBehavior> ();
		m_MaxBalloonDistance = TOXIC_BALLOON_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
