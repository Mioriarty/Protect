using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroy : Destroyable {

	public GameObject child;
	public int childCount = 2;
	public float range = 0.5f;
	public float velocityRange = 2f;

	override public void hit(int strength){
		if(!destroyingAll && strength != GROUND_HIT)
			for(int i = 0; i < childCount; i++) {
				float velFactor = ((float)i / (float)(childCount - 1) - 0.5f) * 2f;
				float x = range * 2f * (float) i / (float) (childCount - 1) - range + transform.position.x;
				GameObject o = Instantiate (child, new Vector3 (x, transform.position.y - 0.3f, 1f), Quaternion.identity);
				ChildOb cOb = o.GetComponent<ChildOb> ();
				if (cOb != null) {
					cOb.movement.x = velFactor * velocityRange;
					cOb.movement.y *= Random.Range (0.8f, 1.3f);
				}
			}

		destroy (strength >= 0);
	}
}
