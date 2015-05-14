using UnityEngine;
using System.Collections;

public class ToxicBalloon : Balloon {

    override public void Init()
    {
        base.Init();
        ChangeColor(Color.green);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
