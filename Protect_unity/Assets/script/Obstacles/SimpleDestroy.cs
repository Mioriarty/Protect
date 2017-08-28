using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : Destroyable {

	override public void hit(int strength){
		destroy (strength >= 0);
	}


}
