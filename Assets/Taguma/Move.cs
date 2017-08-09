using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	float time = 0;
	float InputTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.W)) {
			//transform.Translate (-0.2f,0,0);
			transform.position = transform.position + new Vector3(0, 0, 0.2f);
		}
		if(Input.GetKey(KeyCode.S)){
			//transform.Translate (0.2f,0,0);
			transform.position = transform.position + new Vector3(0, 0, -0.2f);
		}
		if(Input.GetKey(KeyCode.D)){
			//transform.Translate(0,0,0.2f);
			transform.position = transform.position + new Vector3(0.2f, 0, 0);
		}
		if(Input.GetKey (KeyCode.A)){
			//transform.Translate (0,0,-0.2f);
			transform.position = transform.position + new Vector3(-0.2f, 0, 0);
		}
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp (transform.position.x, -20, 20);
		pos.z = Mathf.Clamp (transform.position.z, -14, 14);
		transform.position = pos;

		time += Time.deltaTime;
		Debug.Log ("Time:" + time);
		Debug.Log ("InputTime:" + (time - InputTime));

		if (Input.GetKey (KeyCode.N)) {
			GetComponent<BoxCollider> ().enabled = false;
			InputTime = time;
			// Trigger ON 
			//(Object).collider.isTrigger = true; 
			// Trigger OFF 
			//(object).collider.isTrigger = false; 
		}
		if ((time - InputTime) >= 1.0) {
			GetComponent<BoxCollider> ().enabled = true;
		}
	}
}

