using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour {

	//private GameObject text;
	private GameObject ps;
	private Vector2 screenSize;
	private Vector2 canvasSize;

	void Start () {
		//text = transform.GetChild (0).gameObject;
		ps = transform.GetChild (1).gameObject;

		ps.transform.parent = null;

		screenSize = new Vector2 (Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);
		canvasSize = transform.root.gameObject.GetComponent<CanvasScaler> ().referenceResolution;
		Debug.Log (GetComponent<RectTransform>().anchoredPosition.x / canvasSize.x);
		setPsPos ();

	}

	private void setPsPos(){
		ps.transform.position = new Vector3 (GetComponent<RectTransform>().anchoredPosition.x / canvasSize.x * screenSize.x, GetComponent<RectTransform>().anchoredPosition.y / canvasSize.y * screenSize.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		setPsPos ();
	}

	void OnDestroy(){
		Destroy (ps);
	}	

}
