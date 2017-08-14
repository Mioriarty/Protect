using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	private int points = 0;
	public GameObject scoreboard;
	public GeneratorScript gen;
	public float sceneTransTime = 3;
	private bool isSceneTrans = false;
	private float sceneTransTimePast = 0.0f;

	void Update(){
		
		if (isSceneTrans) {
			sceneTransTimePast += Time.deltaTime;
			if (sceneTransTimePast >= sceneTransTime) {
				SceneManager.LoadScene("EndGameScene", LoadSceneMode.Single);
				gameObject.SetActive (false);
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
	}
}
