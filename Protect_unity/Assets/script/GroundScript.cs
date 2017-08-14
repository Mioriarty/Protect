using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundScript : MonoBehaviour {


	public int startHealth = 10;
	private int health;

	public Color fullHealthColor;
	public Color noHealthColor;

	private SpriteRenderer sp;
	private Animator animator;

	private PlayerScript player;

	public float colorTransTime = 5f;
	public float colorTransAmount = 0.25f;
	private float colorTransTimePast = 0.0f;
	private bool isColorTrans = false;
	private Color[] transColors = new Color[2];

	private Color rawColor;

	public ParticleSystem exterminatePS;


	public BarScript healthbar;



	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
		player = GameObject.FindWithTag ("Orga").GetComponent<PlayerScript> ();
		health = startHealth;
		sp.color = fullHealthColor;
		rawColor = new Color(0f, 0f, 0f);

	}

	void Update(){
		if(isColorTrans){
			colorTransTimePast += Time.deltaTime;
			if (colorTransTimePast >= colorTransTime) {
				colorTransTimePast = 0.0f;
				sp.color = transColors [1];
				isColorTrans = false;
			} else {
				sp.color = Color.Lerp (transColors [0], transColors [1], colorTransTimePast / colorTransTime);
			}
		}



	}

	public void hit(int strength, Color color){
		animator.Play ("Wabble");

		health -= strength;

		healthbar.applyChange ((float)health / (float)startHealth);

		if (health <= 0) {
			if(rawColor == new Color(0f, 0f ,0f))
				sp.color = color;
			die ();
		}
		else
			doColorTrans (color,  1f + 0.5f * (strength - 1f));

		
	}

	public void heal(int amount){
		health += amount;
		health = Mathf.Min (health, startHealth);

		healthbar.applyChange ((float)health / (float)startHealth);

		doColorTrans (fullHealthColor);

	}

	void doColorTrans(Color color, float weight = 1){
		if (rawColor == new Color (0f, 0f, 0f))
			rawColor = color;
		else if (color != fullHealthColor)
			rawColor = Color.Lerp (rawColor, color, colorTransAmount * weight);


		float rh = (float)health / (float)startHealth;
		colorTransTimePast = 0.0f;
		transColors [0] = sp.color;
		transColors [1] = Color.Lerp (fullHealthColor, rawColor, 1-rh);
	//	transColors [1] = Color.Lerp (noHealthColor, fullHealthColor, rh);
		isColorTrans = true;
	}

	void die(){
		ParticleSystem.MainModule newMain = exterminatePS.main;
		exterminatePS.startColor = sp.color;
		exterminatePS.Play ();
		Destroyable.destroyAll ();
		Item.doVanishingAll ();
		player.lose ();
		gameObject.SetActive (false);
	}


	

}
