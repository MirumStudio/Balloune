using UnityEngine;
using System.Collections;

public class LifeBalloon : Balloon {

	public const float LIFE_ROPE_DISTANCE = 4f;
	override public void Init(EBalloonType pType)
    {
		base.Init(pType);
        ChangeColor(Color.red);
        AddBehavior<LifeBehavior>();
        AddBehavior<CharacterPullBehavior>();
		m_MaxRopeDistance = LIFE_ROPE_DISTANCE;
	}
	
	void Update () {
	    
	}
}
