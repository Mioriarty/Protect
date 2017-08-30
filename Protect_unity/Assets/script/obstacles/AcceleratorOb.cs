using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceleratorOb : ObScript {

	public float accFactor = 1.4f;

	override protected void move(){
		if (Input.GetMouseButtonDown (0))
			movement.y *= accFactor;

		base.move ();

	}




}
