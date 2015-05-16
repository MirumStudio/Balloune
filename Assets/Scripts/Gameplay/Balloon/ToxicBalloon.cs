using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {

    override public void Init()
    {
        base.Init();
		ChangeColor(Color.green);
		AddBehavior<DetachBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
