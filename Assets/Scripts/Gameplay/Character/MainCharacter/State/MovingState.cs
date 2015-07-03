using UnityEngine;
using System.Collections;

public class MovingState : CharacterState {

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		AddForce (Vector2.right * animator.GetFloat("Speed") * 100);
	}
}
