using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLiveDestroy : Destroyable {

	public int health = 2;

	override public void hit(int strength){
		health -= strength;
		if (health <= 0)
			destroy (true);
	}
}
