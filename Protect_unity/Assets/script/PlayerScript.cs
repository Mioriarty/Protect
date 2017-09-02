using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
	NONE,
	NORMAL,
	SUPER_SPEED,
	DYING
}

public class PlayerScript : MonoBehaviour {

	private int points = 0;
	public GameObject scoreboard;
	public GeneratorScript gen;
	public float sceneTransTime = 3;
	private bool isSceneTrans = false;
	private float sceneTransTimePast = 0.0f;

	public float superSpeed = 2f;
	public float touchDurationForSuperSpeed = 1.5f;

	private GameState gameState = GameState.NONE;



	void Start(){
		SoundControl.updateSound ();
		gameState = GameState.NORMAL;
	}


	void Update(){
		
		if (isSceneTrans) {
			sceneTransTimePast += Time.deltaTime;
			if (sceneTransTimePast >= sceneTransTime) {
				SceneManager.LoadScene("EndGameScene", LoadSceneMode.Single);
				gameObject.SetActive (false);
			}
		}


		if (ClickManager.instance.getLongestPress () >= touchDurationForSuperSpeed) {
			Time.timeScale = superSpeed;
			gameState = GameState.SUPER_SPEED;
		} else {
			Time.timeScale = 1f;
			gameState = GameState.NORMAL;
		}
	}

	public void addPoints(int amount){
		points += amount;
		scoreboard.GetComponent<Animator> ().Play ("PointAnim");
		scoreboard.GetComponent<Text>().text = points.ToString (); 
	}

	public void lose(){
		Debug.Log ("You lost");
		gen.stopGenerating ();
		PlayerPrefs.SetInt ("points", points);
		isSceneTrans = true;
	}
}
