using UnityEngine;
using System.Collections;

public class JumpingState : CharacterState {
	[SerializeField]
	protected float m_JumpForce = 850f;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
		mBody.AddForce(new Vector2(0f, m_JumpForce));
		Debug.Log ("lol");
		animator.SetBool ("HaveToJump", false);

	}

	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	}
}
