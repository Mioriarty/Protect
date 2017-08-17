using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SimpleGeneratorScript : MonoBehaviour {

	private float genHeight;
	private float genRange;

	public float genSaveDistance = 0.5f;

	public float genTime;
	private float genTimePast = 0.0f;

	public List<GameObject> obstacles;

	private bool generating = true;

	// Use this for initialization
	void Start () {
		genHeight = Camera.main.orthographicSize;
		genRange = genHeight * Screen.width / Screen.height;

	}
		
	
	// Update is called once per frame
	void Update () {
		if (generating) {
			genTimePast += Time.deltaTime;
			if (genTimePast >= genTime) {
				instantiateObstacle (obstacles [Random.Range (0, obstacles.Count)], Random.Range (-1f, 1f));
				genTimePast = 0;
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
