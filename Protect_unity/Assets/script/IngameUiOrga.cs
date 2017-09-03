using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUiOrga : MonoBehaviour {

	public GameObject pauseScreen;
	public Button pauseButton;


	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		if (!pauseButton.enabled && PlayerScript.instance.isGameRunning ())
			pauseButton.enabled = true;
		if (PlayerScript.instance.getGameState () == GameState.PAUSE && Input.GetMouseButtonDown (0))
			cancelPause ();
		
	}
	
	public void callPause(){
		if (PlayerScript.instance.isGameRunning ()) {
			PlayerScript.instance.updateGameState (GameState.PAUSE);
			pauseScreen.SetActive (true);
			pauseButton.enabled = false;
		}
	}

	private void cancelPause(){
		PlayerScript.instance.updateGameState (GameState.NORMAL);
		pauseScreen.SetActive (false);
	}
		
}
