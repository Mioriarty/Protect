using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {
	NONE,
	NORMAL,
	STARTING,
	SUPER_SPEED,
	PAUSE,
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


	public static PlayerScript instance;

	void Awake(){
		instance = this;
	}



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

		if (isGameRunning ()) {

			if (ClickManager.instance.getLongestPress () >= touchDurationForSuperSpeed) {
				updateGameState (GameState.SUPER_SPEED);
			} else {
				updateGameState (GameState.NORMAL);
			}
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
		updateGameState(GameState.DYING);
	}

	public bool updateGameState(GameState state){
		if (gameState == state)
			return false;

		switch (state) {
		case GameState.DYING:
			Time.timeScale = 1f;
			break;
		case GameState.PAUSE:
			Time.timeScale = 0.00000001f;
			break;
		case GameState.NORMAL:
			Time.timeScale = 1f;
			break;
		case GameState.SUPER_SPEED:
			Time.timeScale = superSpeed;
			break;
		}

		gameState = state;

		return true;
	}


	public GameState getGameState(){
		return gameState;
	}

	void OnDestroy(){
		instance = null;
	}

	public bool isGameRunning(){
		return gameState == GameState.NORMAL || gameState == GameState.SUPER_SPEED;
	}


}
