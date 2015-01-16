using UnityEngine;
using System.Collections;

public class TestMainCharacterController : TestBaseCharacterController
{

	

    protected override float GetHorizontalAxisValue()
    {
        float value = Input.GetAxis("Horizontal");

        if (value < 0)
        {
            return -1;
        }
        else if (value > 0)
        {
            return 1;
        }

        return value;
    }
}
