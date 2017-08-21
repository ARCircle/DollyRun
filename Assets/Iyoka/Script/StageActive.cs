using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageActive{

	public static bool isTrue(){
		if (GrobalClass.pause || GrobalClass.gameover || GrobalClass.StartInterval > 0) {
			return false;
		} else {
			return true;
		}
	}
}
