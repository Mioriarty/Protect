using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChellengesMenuOrga : MonoBehaviour {

	public GameObject chButtonPrefab;
	public GameObject chButtonsPanel;

	private int chCount = 20;

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= chCount; i++) {
			GameObject o = Instantiate (chButtonPrefab, chButtonsPanel.transform);
			o.GetComponent<Text> ().text = i.ToString ();
			o.gameObject.GetComponent<ChButtonScript> ().setIndex (i);
		}
	}

	public void clickOnChellenge(int i){
		Debug.Log ("Start chellenge " + i);
	}

}
