using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettungsOrga : MonoBehaviour {

	public GameObject soundOnButton;
	public GameObject soundOffButton;

	// Use this for initialization
	void Start () {
		bool soundOn = PlayerPrefs.GetInt ("soundOn", 1) == 1;
		soundOffButton.GetComponent<CanvasRenderer> ().SetAlpha (soundOn ? 0.3f : 1.0f);
		soundOnButton.GetComponent<CanvasRenderer> ().SetAlpha (soundOn ? 1.0f : 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		Start ();
	}

	public void callBack(){
		MangeManu.instance.startMainMenu ();
	
	}
		

	public void callSound(bool active){
		if (active) {
			soundOffButton.GetComponent<CanvasRenderer> ().SetAlpha (0.3f);
			soundOnButton.GetComponent<CanvasRenderer> ().SetAlpha (1.0f);
			PlayerPrefs.SetInt ("soundOn", 1);
		} else {
			soundOnButton.GetComponent<CanvasRenderer> ().SetAlpha (0.3f);
			soundOffButton.GetComponent<CanvasRenderer> ().SetAlpha (1.0f);
			PlayerPrefs.SetInt ("soundOn", 0);
		}
		SoundControl.updateSound ();

	}

	public void callReset(){
		
		PlayerPrefs.DeleteAll ();
		callSound (true);
	}

}
