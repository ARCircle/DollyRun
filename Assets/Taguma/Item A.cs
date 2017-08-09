using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemA : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.N)){
			GetComponent<BoxCollider>().enabled= false;
			// Trigger ON 
			//(Object).collider.isTrigger = true; 
			// Trigger OFF 
			//(object).collider.isTrigger = false; 
	}
}
}
