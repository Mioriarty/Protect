using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBehavior : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("End");
		Destroy (animator.gameObject);
	}
		
}
