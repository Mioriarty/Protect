using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Destroyable : MonoBehaviour {

	public GameObject psPrefab;
	public Color particleColor = new Color(1, 1, 1, 1);
	private static Color richTrailPsColor = new Color (1f, 0.9f, 0.5f);
	private static Color deadlyTrailPsColor = new Color (1f, 0.65f, 0.6f);
	public int worthyness = 1;

	private PlayerScript player;

	public abstract void hit (int strength);

	private static List<Destroyable> destrs = new List<Destroyable> ();

	public GameObject trailPS;

	private GameObject realTrailPS;

	private bool clicked = false;

	private bool deadlyParticlesRequested = false;

	protected static bool destroyingAll = false;

	public static int GROUND_HIT = -3;


	void Start(){
		player = GameObject.FindWithTag ("Orga").GetComponent<PlayerScript> ();
		realTrailPS = Instantiate (trailPS);
		if (worthyness > 1) {
			ParticleSystem.MainModule main = realTrailPS.GetComponent<ParticleSystem>().main;
			main.startColor = richTrailPsColor;
		}

		if (deadlyParticlesRequested)
			enableRedParticles();

		
	}

	public void enableRedParticles(){
		if (realTrailPS == null) {
			deadlyParticlesRequested = true;
		} else {
			ParticleSystem.MainModule main = realTrailPS.GetComponent<ParticleSystem>().main;
			main.startColor = deadlyTrailPsColor;
		}

	}
	
	void OnEnable(){
		destrs.Add (this);	
	}
	
	void OnDisable(){
		destrs.Remove (this);	
	}

	void Update(){
		setPsPos ();
		clicked = false;
		if (transform.position.y < -Camera.main.orthographicSize - 2.0f) {
			realTrailPS.GetComponent<ParticleSystem> ().Stop ();
			Destroy (gameObject);
		}
	}

	protected void destroy(bool givePoints) {
		ParticleSystem ps = Instantiate (psPrefab, transform.position, Quaternion.Euler (270, 0, 0)).GetComponent<ParticleSystem> ();
		ParticleSystem.MainModule newMain = ps.main;
		newMain.startColor = new ParticleSystem.MinMaxGradient(particleColor );
		ps.Play ();

		if(player != null && givePoints)
			player.addPoints (worthyness);

		if(realTrailPS != null)
			realTrailPS.GetComponent<ParticleSystem> ().Stop ();
		
		Destroy (gameObject);
	}

	void setPsPos(){
		realTrailPS.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f);
	}
		

	void OnMouseDown(){
		if (SystemInfo.deviceType == DeviceType.Desktop)
			click ();

	}


	public static void destroyAll(){
		destroyingAll = true;
		for (int i = destrs.Count - 1; i >= 0; i--)
			if(destrs[i].enabled)
				destrs [i].destroy (false);
		
		destrs.Clear ();
		destroyingAll = false;
	}

	public void click(){
		if (!clicked) {
			clicked = true;
			hit (1);
		}
	}

}
