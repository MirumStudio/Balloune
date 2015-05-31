using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TouchControl
{
    public Touch mTouch;

    public void info()
    {
        if (!mTouch.IsNull())
        {
            Debug.Log("FingerId: " + mTouch.fingerId.ToString() + ", Phase: " + mTouch.phase.ToString() + ", Tapcount: " + mTouch.tapCount.ToString());

            if(mTouch.tapCount > 1)
            {
                int y = 6;
            }
        }
    }


    
}
