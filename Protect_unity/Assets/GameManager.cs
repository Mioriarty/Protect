using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float superSpeed = 2f;
	public float touchDurationForSuperSpeed = 1.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ClickManager.instance.getLongestPress () >= touchDurationForSuperSpeed)
			Time.timeScale = superSpeed;
		else
			Time.timeScale = 1f;
	}
}
