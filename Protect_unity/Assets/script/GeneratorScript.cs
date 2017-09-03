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
	public float itemChance = 1f;
	private float itemReloadTimePast = 0f;

	private float startTime;

	public List<GameObject> obstacles;
	public List<GameObject> items;
	private float[] times;
	private int crntLevel = 0;

	/*
		0: basic
		1: fat and bloody
		2: speedy
		3: sin mover
		4: zigzag fat
		5: zigzag speedy
		6: more speed
		7: blow up
		8: more lifes

	*/

	private Level[] levels = new Level[]{
		new Level( // 0 Intro #0
			0.0f,
			new float[]{1.1f}),
		new Level( // 1 Intro #1
			9.0f,
			new float[]{1.53f, 2.1f}),
		new Level( // 2 Intro #2
			22.0f,
			new float[]{3f, 1.8f, 2.03f}),
		new Level( // 3 Showoff #2
			33.0f,
			new float[]{2f, 2.2f, 1.8f}),
		new Level( // 4 Intro #3
			40.0f,
			new float[]{1.2f, 2.5f, 3.49f, 3.3f}),
		new Level( // 5 Intro #4
			55.0f,
			new float[]{3.02f, 2.76f, -1f, 1.77f, 2.03f}),
		new Level( // 6 Intro #5
			64.2f,
			new float[]{2.83f, 3.87f, 3.49f, 3.5f, 3.37f, 4.03f}),
		new Level( // 7 Intro #7, #8
			82.0f,
			new float[]{2.83f, 3.91f, -1f, -1f, 7.3f, -1f, -1f, 5.32f, 7.66f}),
		new Level( // 8 Showoff #7
			105.0f,
			new float[]{4.76f, -1f, -1f, 5.32f, 6.03f, 3.42f, -1f, 5.32f}),
		new Level( // 9 Intro #6
			115.0f,
			new float[]{-1f, 5.01f, -1f, -1f, -1f, -1f, 4.37f, 7.23f, 4.02f}),
		new Level( // 10 abfuck
			135.0f,
			new float[]{6.78f, 7.45f, 4.38f, 5.67f, 6.53f, 6.35f, 9.71f, 5.12f, 8.3f})

	};

	private bool generating = true;


	// Use this for initialization
	void Start () {
		genHeight = Camera.main.orthographicSize;
		genRange = genHeight * Screen.width / Screen.height;
		times = new float[obstacles.Count];

	}

	void OnEnable(){
		startTime = Time.time - (crntLevel == 0 ? 0f : levels[crntLevel-1].start);
	}
	
	// Update is called once per frame
	void Update () {
		if (generating) {
			if (crntLevel < levels.Length - 1 && Time.time - startTime > levels [crntLevel + 1].start) {
				Debug.Log ("Level Up " + (crntLevel+1));
				times = new float[obstacles.Count];
				crntLevel++;
				for (int i = 0; i < levels[crntLevel].delays.Length; i++) {
					float delay = levels [crntLevel].delays [i];
					if (delay <= 0f)
						continue;
					times [i] = Random.Range (-delay*0.5f, delay*0.9f);
				}

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

		Instantiate(prefab, new Vector3(Mathf.Lerp(0, range*2, (x+1)/2f)-range, genHeight + height + genSaveDistance, 1f), Quaternion.identity);
	}

	public void stopGenerating(){
		generating = false;
	}


}
