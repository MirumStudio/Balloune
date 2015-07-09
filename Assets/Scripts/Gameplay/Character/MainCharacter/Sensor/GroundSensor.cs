using UnityEngine;
using System.Collections;

public class GroundSensor : CharacterSensor {

	
	// Update is called once per frame
	void Update () {
		bool grounded = Physics2D.Linecast(GetBottomLeftCorner(), GetBottomRightCorner(), GroundLayerMask)
			|| Physics2D.Linecast(GetBottomLeftCorner(), GetBottomRightCorner(), PlateformLayerMask);

		Debug.DrawLine(GetBottomLeftCorner(), GetBottomRightCorner(), Color.red);
		mAnimator.SetBool ("IsGrounded", grounded);
	}
}
