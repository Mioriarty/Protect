using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

	void Update(){
		foreach (Touch t in Input.touches) {
			if (t.phase != TouchPhase.Began)
				continue;
			castClick (t.position);


		}
	}


	private void castClick(Vector2 pos){
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
		if (hit != null && hit.collider != null) {
			GameObject o = hit.collider.gameObject;
			if (o.tag == "Obstacle") {
				o.GetComponent<Destroyable> ().click ();
			}

		}
	}


}


