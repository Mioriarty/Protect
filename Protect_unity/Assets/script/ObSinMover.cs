using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObSinMover : ObScript {

	public float speed;

	private float timeOffset;

	override protected void init(){
		timeOffset = Random.value;
	}


	override protected void move(){
		Vector3 v = transform.position;
		v.x = xStart + Mathf.Sin ((Time.time + timeOffset) * speed) * movement.x;
		v.y += movement.y * Time.deltaTime;
		transform.position = v;
	}

}
