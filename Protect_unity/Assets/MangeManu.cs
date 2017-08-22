using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MangeManu : MonoBehaviour {

	public Text scoreboard;
	public List<GameObject> buttons;
	private List<Color> buttonTextsColors;

	private short callState = 0;
	public float callWaitTime = 1.5f;
	private float callTimePast = 0.0f;

	private const short PLAY = 1;
	private const short UPGRADES = 2;

	// Use this for initialization
	void Start () {
		scoreboard.text = PlayerPrefs.GetInt ("highscore", 0).ToString();
		buttonTextsColors = new List<Color> ();
		foreach(GameObject o in buttons){
			buttonTextsColors.Add(o.transform.GetChild (0).gameObject.GetComponent<Text> ().color);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (callState != 0) {
			callTimePast += Time.deltaTime;
			if (callTimePast >= callWaitTime) {
				switch (callState) {
				case PLAY:
					SceneManager.LoadScene ("GameScene", LoadSceneMode.Single);
					break;
				case UPGRADES:
					Debug.Log ("Upgrades");
					break;
				
				}
				callState = 0;
				callTimePast = 0.0f;
			}


			for(int i = 0; i < buttons.Count; i++){
				Color c = buttonTextsColors[i];
				if (callTimePast == 0)
					c.a = 0.0f;
				else
					c.a = 1 - callTimePast / callWaitTime;
				buttons[i].transform.GetChild (0).gameObject.GetComponent<Text> ().color = c;
			}

		}

	}

	public void callPlay(){
		prepareCall ();
		callState = PLAY;

	}

	public void callUpgrades(){
		//prepareCall ();
		callState = UPGRADES;
	}

	public void callSettings(){
		SceneManager.LoadScene ("SettingsScene", LoadSceneMode.Single);
	}

	void prepareCall(){
		foreach (GameObject o in buttons) {
			o.GetComponent<Button> ().interactable = false;
		}
		Destroyable.destroyAll ();
		gameObject.GetComponent<SimpleGeneratorScript> ().stopGenerating ();
	}
}
