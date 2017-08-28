using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meterEffectScript : MonoBehaviour {

	private Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		Debug.Log ((int)GrobalClass.distance);
		if ((int)GrobalClass.distance == 100 ||
			(int)GrobalClass.distance == 200 ||
			(int)GrobalClass.distance == 300 ||
			(int)GrobalClass.distance == 400 ||
			(int)GrobalClass.distance == 500 ||
			(int)GrobalClass.distance == 750 ||
			(int)GrobalClass.distance == 1000 ||
			(int)GrobalClass.distance == 1500 ||
			(int)GrobalClass.distance == 2000 ||
			(int)GrobalClass.distance == 3000 ||
			(int)GrobalClass.distance == 4000 ||
			(int)GrobalClass.distance == 5000 ||
			(int)GrobalClass.distance == 6000 ||
			(int)GrobalClass.distance == 7000 ||
			(int)GrobalClass.distance == 8000 ||
			(int)GrobalClass.distance == 9000 ||
			(int)GrobalClass.distance == 10000 ||
			(int)GrobalClass.distance == 15000 ||
			(int)GrobalClass.distance == 20000 ||
			(int)GrobalClass.distance == 25000 ||
			(int)GrobalClass.distance == 30000 ||
			(int)GrobalClass.distance == 35000 ||
			(int)GrobalClass.distance == 40000 ||
			(int)GrobalClass.distance == 45000 ||
			(int)GrobalClass.distance == 50000 ||
			(int)GrobalClass.distance == 60000 ||
			(int)GrobalClass.distance == 70000 ||
			(int)GrobalClass.distance == 80000 ||
			(int)GrobalClass.distance == 90000 ||
			(int)GrobalClass.distance == 99999) {
			anim.SetTrigger ("Anim_Play");
		}
	}
}
