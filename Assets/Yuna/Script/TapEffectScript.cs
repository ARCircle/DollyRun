using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffectScript : MonoBehaviour {

	private ParticleSystem ps;
	private GameObject cam;

	void Start () {
		ps = GetComponent<ParticleSystem> ();
		cam = GameObject.Find ("Main Camera");
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition + cam.transform.forward * 10); 
			transform.position = pos; 
			ps.Emit (1); 
		}
	}
}
