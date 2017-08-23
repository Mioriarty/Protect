﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {


	void Update(){
		foreach (Touch t in Input.touches) {
			if (t.phase != TouchPhase.Began)
				continue;
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(t.position), Vector2.zero);
			if (hit != null && hit.collider != null) {
				GameObject o = hit.collider.gameObject;
				if (o.tag == "Obstacle") {
					o.GetComponent<Destroyable> ().click ();
				}

			}

		}
	}


}


