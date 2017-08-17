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
				GameObject tmp = Instantiate<GameObject> (breakeffect);
				tmp.transform.position = new Vector3 (this.transform.position.x, 0f, this.transform.position.z);
				Destroy (this.gameObject);
			} else {
				col.gameObject.SetActive(false);
				GrobalClass.gameover = true;
				Instantiate (burst).transform.position = col.gameObject.transform.position;
			}
		}
	}
}