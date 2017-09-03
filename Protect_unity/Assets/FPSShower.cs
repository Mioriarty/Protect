using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class FPSShower : MonoBehaviour {

	private Text text;

	private int frameCounter = 0;
	private float lastFrameTime = 0f;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		if (PlayerPrefs.GetInt ("debugMode", 0) != 1)
			gameObject.SetActive (false);
		else 
			StartCoroutine ("displayFPS");
	}

	IEnumerator displayFPS(){
		yield return new WaitForSecondsRealtime (0.4f);
		float deltaTime = Time.unscaledTime - lastFrameTime;
		float fps = (float)frameCounter / deltaTime;
		lastFrameTime = Time.unscaledTime;
		frameCounter = 0;
		text.text = fps.ToString ("0.00") + " FPS";
		StartCoroutine ("displayFPS");
	}
	
	// Update is called once per frame
	void Update () {
		frameCounter++;
	}

	public void updateVisibility(){
		if (PlayerPrefs.GetInt ("debugMode", 0) != 1) {
			StopCoroutine ("displayFPS");
			gameObject.SetActive (false);
		} else {
			if (!gameObject.activeSelf) {
				gameObject.SetActive (true);
				frameCounter = 0;
				lastFrameTime = Time.time;
				StartCoroutine ("displayFPS");
			}
		}
	}
}
