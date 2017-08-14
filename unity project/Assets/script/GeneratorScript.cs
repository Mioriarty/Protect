using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level {
	public float start;
	public float[] delays;

	public Level(float s, float[] d){
		start = s;
		delays = d;
	}
}

public class GeneratorScript : MonoBehaviour {

	private float genHeight;
	private float genRange;

	public float genSaveDistance = 0.5f;

	public float itemReloadTime = 1f;
	public float itemChance = 0.1f;
	private float itemReloadTimePast = 0f;

	private float startTime;

	public List<GameObject> obstacles;
	public List<GameObject> items;
	private float[] times;
	private int crntLevel;
	private Level[] levels = new Level[]{
		new Level(
			0.0f,
			new float[]{1.1f}),
		new Level(
			9.0f,
			new float[]{1.53f, 2.1f}),
		new Level(
			22.0f,
			new float[]{3f, 1.8f, 2f}),
		new Level(
			33.0f,
			new float[]{2f, 2.2f, 1.8f}),
		new Level(
			40.0f,
			new float[]{1.2f, 2.5f, 3.49f, 3.3f}),
		new Level(
			55.0f,
			new float[]{3.02f, 2.76f, -1f, 1.2f, 2.74f})

	};

	private bool generating = true;

	// Use this for initialization
	void Start () {
		genHeight = Camera.main.orthographicSize;
		genRange = genHeight * Screen.width / Screen.height;
		times = new float[obstacles.Count];

	}

	void OnEnable(){
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (generating) {
			if (crntLevel < levels.Length - 1 && Time.time - startTime > levels [crntLevel + 1].start) {
				Debug.Log ("Level Up " + (crntLevel+1));
				times = new float[obstacles.Count];
				crntLevel++;
			}
			Level level = levels [crntLevel];
			for (int i = 0; i < times.Length; i++) {
				if (i >= level.delays.Length)
					break; // Level doesn't support these objects

				if (level.delays [i] <= 0)
					continue;
				
				float time = times [i];
				time += Time.deltaTime;

				if (time >= level.delays [i]) {
					instantiateObstacle (obstacles [i], Random.Range (-1.0f, 1.0f));
					time -= level.delays [i];
				}
				times [i] = time;
			}

			// Items
			itemReloadTimePast += Time.deltaTime;
			if (itemReloadTimePast >= itemReloadTime) {
				itemReloadTimePast -= itemReloadTime;

				if (Random.value <= itemChance) {
					int itemIndex = Random.Range (0, items.Count);
					instantiateObstacle (items [itemIndex], Random.Range (-1.0f, 1.0f));
				}
			}
		}
	}

	void instantiateObstacle(GameObject prefab, float x){
		SpriteRenderer sp = prefab.GetComponent<SpriteRenderer> ();
		if (sp == null)
			sp = prefab.GetComponentInChildren<SpriteRenderer> ();
		Vector3 size = sp.sprite.bounds.size;
		float height = size.y * prefab.transform.localScale.y;
		float width = size.x * prefab.transform.localScale.x;
		float range = genRange - width - genSaveDistance;
		Instantiate(prefab, new Vector3(Mathf.Lerp(0, range*2, (x+1)/2f)-range, genHeight + height + genSaveDistance, prefab.transform.position.z), Quaternion.identity);
	}

	public void stopGenerating(){
		generating = false;
	}


}
