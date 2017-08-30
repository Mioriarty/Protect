using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObScript : MonoBehaviour {

	public Vector3 movement;
	public float groundHitDamage = 1;
	public Destroyable destroyable;

	protected float xStart;


	// Use this for initialization
	void Start () {
		xStart = transform.position.x;
		float z = transform.localScale.x + transform.localScale.y;
		transform.position = new Vector3 (xStart, transform.position.y, z);
		if (groundHitDamage > 1)
			destroyable.enableRedParticles ();
		init ();
	}

	protected virtual void init(){}
	
	// Update is called once per frame
	void Update () {
		move ();
	}

	protected virtual void move(){
		transform.Translate (movement * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D c){
		if (c == null || !c.gameObject.activeSelf)
			return;
		if(!c.gameObject.activeSelf)
			return;	
		if (c.gameObject.tag == "Ground") {
			GameObject.Find ("Ground").GetComponent<GroundScript> ().hit (groundHitDamage, gameObject.GetComponent<SpriteRenderer> ().color);
			destroyable.hit (Destroyable.GROUND_HIT);
		}
	}


}
