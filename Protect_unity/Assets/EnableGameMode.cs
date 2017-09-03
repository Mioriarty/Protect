using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameMode : StateMachineBehaviour {

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		PlayerScript.instance.updateGameState (GameState.STARTING);
	}



	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		PlayerScript.instance.updateGameState (GameState.NORMAL);
	}


}
