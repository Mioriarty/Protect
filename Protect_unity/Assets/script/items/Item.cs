using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

	private GameObject root;
	private Animator animator;
	private bool vanishing = false;
	private float animTime = 0.0f;

	public float fallSpeed = 2;

	protected bool reactOnClick = true;

	private static List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		root = transform.parent.gameObject;
		animator = GetComponent<Animator> ();

		init ();
	}
	
	void OnEnable(){
		items.Add(this);	
	}
	
	void OnDisable(){
		items.Remove(this);
	}

	protected abstract void init (); 
	
	// Update is called once per frame
	void Update () {
		if (vanishing) {
			animTime += Time.deltaTime;
			if (animTime > 0.6f) {
				Destroy (root);
			}
		} else {
			root.transform.Translate (0, -fallSpeed * Time.deltaTime, 0);
		}

		if (root.transform.position.y < -Camera.main.orthographicSize - 3)
			Destroy (root);
	}
		

	void OnMouseDown(){
		if (reactOnClick)
			activate ();
	}

	protected abstract void activate ();

	protected void doVanishing(){
		if (!vanishing) {
			animator.Play ("ItemVanish");
			vanishing = true;
		}
	}

	public static void doVanishingAll(){
		for (int i = items.Count - 1; i >= 0; i--)
			items [i].doVanishing ();

		items.Clear ();
	}
}
