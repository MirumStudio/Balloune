using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#if UNITY_IOS
public class TouchService : iOSTouchService
#else
public class TouchService : DefaultTouchService
#endif
{
    protected override void OnTouchBegan(Touch pTouch)
    {
        //tap or double tap
    }

    protected override void OnTouchMoved(Touch pTouch)
    {
        //swipe begin
        //circle draw update
    }

    protected override void OnTouchStationary(Touch pTouch)
    {
        //end swipe
    }

    protected override void OnTouchEnded(Touch pTouch)
    {
        //circle drawnn
        //end swipe
    }

    protected override void OnTouchCanceled(Touch pTouch)
    {
        //Touch cancelled
    }
}
