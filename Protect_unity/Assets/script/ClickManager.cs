using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour {

	public static ClickManager instance;
	public void Awake(){
		instance = this;
	}

	private float longestPress = 0.0f;
	private float[] pressStarts = new float[40];

	void Update(){
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			if (Input.GetMouseButton (0)) {
				longestPress += Time.deltaTime;
				if(Input.GetMouseButtonDown(0))
					castClick (Input.mousePosition);
			} else
				longestPress = 0.0f;
		} else {
			longestPress = 0.0f;
			foreach (Touch t in Input.touches) {
				if (t.phase == TouchPhase.Began) {
					pressStarts [t.fingerId] = Time.time;
				} else {
					longestPress = Mathf.Max (longestPress, Time.time - pressStarts [t.fingerId]);
				}

				if (t.phase != TouchPhase.Began)
					continue;
				castClick (t.position);
			}
		}

		Debug.Log (longestPress);
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

	public float getLongestPress(){
		return longestPress;
	}


}


