using UnityEngine;
using System.Collections;

public class FlyingBalloon : Balloon {
	
	private const float FLYING_BALLOON_DISTANCE = 7f;
	
	override public void Init()
	{
		base.Init();
		ChangeColor(Color.gray);
		AddBehavior<CharacterPullBehavior>();
		AddBehavior<FlyingBehavior>();
		m_MaxBalloonDistance = FLYING_BALLOON_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
