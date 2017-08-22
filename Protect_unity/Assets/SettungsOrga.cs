using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettungsOrga : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void callBack(){
		SceneManager.LoadScene ("MenuScene", LoadSceneMode.Single);
	
	}
}
