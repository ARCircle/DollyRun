using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {
	public GameObject burst;
	public GameObject breakeffect;

	void Start(){
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player"){
			if (GrobalClass.usingAtime > 0f) {
				Instantiate (breakeffect).transform.position = this.transform.position;
				Destroy (this.gameObject);
			} else {
				col.gameObject.SetActive(false);
				GrobalClass.gameover = true;
				Instantiate (burst).transform.position = col.gameObject.transform.position;
			}
		}
	}
}