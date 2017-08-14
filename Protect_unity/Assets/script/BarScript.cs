using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	private Image image;

	public Color full;
	public Color empty;

	void Start(){
		image = GetComponent<Image> ();
		image.color = full;
	}
		


	public void applyChange(float percentage){
		setSize (percentage);
		image.color = Color.Lerp (empty, full, percentage);
	}

	private void setSize(float f){
		image.rectTransform.localScale = new Vector3 (f, 1f, 1f);
	}
}
