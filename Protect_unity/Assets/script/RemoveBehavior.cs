using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBehavior : StateMachineBehaviour {

	public bool removeParent = false;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (removeParent)
			Destroy (animator.gameObject.transform.parent.gameObject);
		else
			Destroy (animator.gameObject);
	}
		
}
