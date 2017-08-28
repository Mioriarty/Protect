using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointOrga : MonoBehaviour {

	public static PointOrga instance;
	void Awake(){instance = this;}

	public Text scoreOut;
	public Text highscoreOut;

	private bool newHighscore = false;

	public float timeOffset = 1;
	private float timeOffsetPast = 0;

	public List<ParticleSystem> highscorePSs;
	public float psPlayOffset = 0.2f;
	private float psTimePast = 0.0f;
	private int psPlayCount = 0;

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt("points");
		int highscore = PlayerPrefs.GetInt ("highscore", 0);
		scoreOut.text = score.ToString();

		if (score > highscore) {
			highscoreOut.text = "New Highscore!";
			PlayerPrefs.SetInt ("highscore", score);
			newHighscore = true;
		} else if (score == highscore && score > 0) {
			highscoreOut.text = "Soo Close!";
		} else {
			highscoreOut.text = "Highscore\n" + highscore.ToString ();
		}
		PlayerPrefs.Save ();
	}

	void Update() {
		timeOffsetPast += Time.deltaTime;
		if(timeOffsetPast >= timeOffset){
			if(Input.GetMouseButtonDown(0))
				SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);

			if (newHighscore && psPlayCount < highscorePSs.Count) {
				psTimePast += Time.deltaTime;
				if (psTimePast >= psPlayCount * psPlayOffset) {
					highscorePSs [psPlayCount].Play ();
					psPlayCount++;

				}
			}
		}
	}
	

}
