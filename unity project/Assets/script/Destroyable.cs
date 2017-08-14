using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Destroyable : MonoBehaviour {

	public GameObject psPrefab;
	public Color particleColor = new Color(1, 1, 1, 1);
	public int worthyness = 1;

	private PlayerScript player;

	public abstract void hit (int strength);

	private static List<Destroyable> destrs = new List<Destroyable> ();

	public GameObject trailPS;

	private GameObject realTrailPS;

	void Start(){
		player = GameObject.FindWithTag ("Orga").GetComponent<PlayerScript> ();
		realTrailPS = Instantiate (trailPS);
		destrs.Add (this);
	}

	void Update(){
		setPsPos ();
	}

	protected void destroy(bool givePoints) {
		ParticleSystem ps = Instantiate (psPrefab, transform.position, Quaternion.Euler (270, 0, 0)).GetComponent<ParticleSystem> ();
		ParticleSystem.MainModule newMain = ps.main;
		ps.startColor = particleColor;
		ps.Play ();

		if(givePoints)
			player.addPoints (worthyness);

		realTrailPS.GetComponent<ParticleSystem> ().Stop ();
		destrs.Remove (this);
		
		Destroy (gameObject);
	}

	void setPsPos(){
		realTrailPS.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
	}

	void OnMouseDown(){
		hit (1);
	}

	public static void destroyAll(){
		for (int i = destrs.Count - 1; i >= 0; i--)
			destrs [i].destroy (false);
		
		destrs.Clear ();
	}
}
