using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChButtonScript : MonoBehaviour {

	private int index = 1;

	public void setIndex(int i){
		index = i;
	}

	public void call(){
		Debug.Log ("Start Chel " + index);
	}
}
