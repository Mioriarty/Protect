using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl {


	public static void updateSound(){
		AudioListener.pause = PlayerPrefs.GetInt ("soundOn", 1) == 0;

	}

}
