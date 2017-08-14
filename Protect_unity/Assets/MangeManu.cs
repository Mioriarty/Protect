using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MangeManu : MonoBehaviour {

	public Text scoreboard;

	// Use this for initialization
	void Start () {
		scoreboard.text = PlayerPrefs.GetInt ("points", 0).ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame(){
		SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
	}
}
