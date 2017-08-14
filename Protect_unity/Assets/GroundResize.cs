using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundResize : MonoBehaviour {

	private const float standartWidth = 2.929688f;

	// Use this for initialization
	void Start () {
		float width = Camera.main.orthographicSize * Screen.width / Screen.height;
		gameObject.transform.localScale = new Vector3(1f / standartWidth * width, 1f, 1f);
	}

}
