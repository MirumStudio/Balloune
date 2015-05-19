using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {
	
	private const float TOXIC_BALLOON_DISTANCE = 2f;

    override public void Init()
    {
        base.Init();
		ChangeColor(Color.green);
		AddBehavior<DetachBehavior>();
		AddBehavior<AttachBehavior>();
		m_MaxBalloonDistance = TOXIC_BALLOON_DISTANCE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
