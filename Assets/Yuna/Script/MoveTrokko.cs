using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrokko : MonoBehaviour {
	
	private Vector3 mousePosi;

	private float x;
	private float z;

	void Start () {
		
	}

	void Update () {
		if (Input.GetMouseButton (0) && Input.mousePosition.y > Screen.height / 5) {
			mousePosi = Input.mousePosition;
			mousePosi.z = 10;
			x = Camera.main.ScreenToWorldPoint (mousePosi).x;
		}
		z += 3 * Time.deltaTime;
		transform.position = new Vector3 (x, 0, z);
	}
}
