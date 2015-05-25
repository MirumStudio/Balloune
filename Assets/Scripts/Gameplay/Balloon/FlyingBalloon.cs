using UnityEngine;
using System.Collections;

public class FlyingBalloon : Balloon {
	
	public const float FLYING_ROPE_DISTANCE = 7f;
	
	override public void Init(EBalloonType pType)
	{
		base.Init(pType);
		ChangeColor(Color.gray);
		AddBehavior<CharacterPullBehavior>();
		AddBehavior<FlyingBehavior>();
		AddBehavior<TriggerableBehavior>();
		m_MaxRopeDistance = FLYING_ROPE_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
