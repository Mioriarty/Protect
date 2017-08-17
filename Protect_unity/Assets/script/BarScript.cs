using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	private Image image;

	public Color full;
	public Color empty;

	private float crntScale = 1.0f;
	private bool shouldSetScale = false;

	void Start(){
		image = GetComponent<Image> ();
		image.color = full;
	}

	void LateUpdate(){
		if(shouldSetScale)
			setSize (crntScale);
	}
		


	public void applyChange(float percentage){
		shouldSetScale = true;
		crntScale = percentage;
		image.color = Color.Lerp (empty, full, percentage);
	}

	private void setSize(float f){
		image.rectTransform.localScale = new Vector3 (f, 1f, 1f);
	}
}
