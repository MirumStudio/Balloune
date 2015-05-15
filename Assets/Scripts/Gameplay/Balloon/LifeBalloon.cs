using UnityEngine;
using System.Collections;

public class LifeBalloon : Balloon {

    override public void Init()
    {
        base.Init();
        ChangeColor(Color.red);
        AddBehavior<LifeBehavior>();
	}
	
	void Update () {
	    
	}
}
