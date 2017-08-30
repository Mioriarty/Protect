using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOb : ObScript {

	public float horizontalGrip;

	public float bounds = 3;

	override protected void move(){
		movement.x *= 1 - (horizontalGrip * Time.deltaTime);

		if (transform.position.x > bounds)
			movement.x = -Mathf.Abs (movement.x);
		else if (transform.position.x < -bounds)
			movement.x = Mathf.Abs (movement.x);

		base.move ();
	}
}
