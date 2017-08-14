using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObScript : MonoBehaviour {

	public Vector3 movement;
	public uint groundHitDamage = 1;
	public Destroyable destroyable;
	private GroundScript groundScript;




	// Use this for initialization
	void Start () {
		groundScript = GameObject.Find ("Ground").GetComponent<GroundScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	protected virtual void move(){
		transform.Translate (movement * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c.gameObject.tag == "Ground") {
			groundScript.hit ((int)groundHitDamage, gameObject.GetComponent<SpriteRenderer> ().color);
			destroyable.hit (-1);
		}
	}


}
