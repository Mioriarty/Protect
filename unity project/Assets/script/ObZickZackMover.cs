using UnityEngine;

public class ObZickZackMover : ObScript {

	private bool moveRight;
	private bool first = true;

	public float changeChance = 0.01f;

	public float bounds = 3;


	override protected void move(){
		if (first) {
			moveRight = Random.value >= 0.5;
			first = false;
		}

		if (Random.value <= changeChance)
			moveRight = moveRight ? false : true;

		if (transform.position.x > bounds)
			moveRight = false;
		else if (transform.position.x < -bounds)
			moveRight = true;

		transform.Translate (new Vector3(moveRight ? movement.x : -movement.x, movement.y, movement.z) * Time.deltaTime);
	}
}
