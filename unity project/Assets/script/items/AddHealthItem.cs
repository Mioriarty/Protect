using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthItem : Item {

	public int healAmount = 1;

	private GroundScript ground;

	protected override void init() {
		ground = GameObject.FindWithTag ("Ground").GetComponent<GroundScript>();
	}

	protected override void activate ()
	{
		reactOnClick = false;
		ground.heal (healAmount);
		doVanishing ();
	}
}
