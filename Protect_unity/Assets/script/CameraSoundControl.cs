using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSoundControl : MonoBehaviour {

	private AudioListener listener;

	// Use this for initialization
	void Start () {
		listener = GetComponent<AudioListener> ();
		updateSound ();
	}

	public void updateSound(){
		listener.enabled = PlayerPrefs.GetInt ("soundOn", 1) == 1;
	}

}
