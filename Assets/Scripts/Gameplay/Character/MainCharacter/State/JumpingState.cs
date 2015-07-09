using UnityEngine;
using System.Collections;

public class JumpingState : CharacterState {
	[SerializeField]
	protected float m_JumpForce = 1000f;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		Debug.Log ("JUMPING STATE: Enter");

		if (mBody.velocity.y == 0) {
		
			float xForce = 50f;
			if(animator.GetBool("IsPlateformJump"))
			{
				xForce = 0f;
				animator.SetBool("IsPlateformJump", false);
			}

			mBody.AddForce (new Vector2 (xForce * Mathf.Sign(animator.GetFloat("Speed")), m_JumpForce));
		}
		animator.SetBool ("HaveToJump", false);

	}

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}
}
