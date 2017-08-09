using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {
	public GameObject burst;

	void Start(){
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player" && GrobalClass.usingAtime <= 0f) {
			col.gameObject.SetActive(false);
			MoveDown.enable = false;
			MoveDownInfinite.enable = false;
			Instantiate (burst).transform.position = col.gameObject.transform.position;
		}
	}
}
