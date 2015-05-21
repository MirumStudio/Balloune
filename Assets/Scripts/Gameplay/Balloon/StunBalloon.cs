using UnityEngine;
using System.Collections;

public class StunBalloon : Balloon {
	
	private const float STUN_BALLOON_DISTANCE = 2f;

    override public void Init()
    {
        base.Init();
		ChangeColor(Color.yellow);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		m_MaxBalloonDistance = STUN_BALLOON_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
