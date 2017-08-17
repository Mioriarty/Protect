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

		if (transform.position.y < -Camera.main.orthographicSize - 2.0f) {
			realTrailPS.GetComponent<ParticleSystem> ().Stop ();
			Destroy (gameObject);
		}
	}

	protected void destroy(bool givePoints) {
		ParticleSystem ps = Instantiate (psPrefab, transform.position, Quaternion.Euler (270, 0, 0)).GetComponent<ParticleSystem> ();
		ParticleSystem.MainModule newMain = ps.main;
		ps.startColor = particleColor;
		ps.Play ();

		if(player != null && givePoints)
			player.addPoints (worthyness);

		realTrailPS.GetComponent<ParticleSystem> ().Stop ();
		
		Destroy (gameObject);
	}

	void setPsPos(){
		realTrailPS.transform.position = new Vector3(transform.position.x, transform.position.y, 2);
	}
		

	void OnMouseDown(){
		hit (1);
	}

	void OnDestroy(){
		destrs.Remove (this);
	}


	public static void destroyAll(){
		for (int i = destrs.Count - 1; i >= 0; i--)
			if(destrs[i].enabled)
				destrs [i].destroy (false);
		
		destrs.Clear ();
	}
}
