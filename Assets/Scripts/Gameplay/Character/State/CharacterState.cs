using UnityEngine;
using System.Collections;
using Radix.Logging;

public class CharacterState : StateMachineBehaviour {

    protected const string IS_PULLING_PARAMATER = "IsPulling";
    protected const string SPEED_PARAMATER = "Speed";
    protected const string GROUND_PARAMATER = "IsGrounded";
    protected const string JUMP_PARAMATER = "HaveToJump";
    protected const string PLATFORM_PARAMETER = "IsPlateformJump";
    protected const string HOLE_PARAMATER = "IsHoleJump";

	protected Rigidbody2D mBody = null;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		mBody = animator.GetComponent<Rigidbody2D> ();

        Log.Debug(animator.name + " enter " + GetType().ToString(), ELogCategory.CHARACTER_STATE);
	}
	
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
	
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Log.Debug(animator.name + " exit " + GetType().ToString(), ELogCategory.CHARACTER_STATE);
	}
	
	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
	
	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	protected void AddForce(Vector2 pVector)
	{
		mBody.AddForce (pVector);
	}
}
