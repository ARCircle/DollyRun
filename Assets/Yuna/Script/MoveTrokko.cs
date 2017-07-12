using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrokko : MonoBehaviour {
	
	public Vector2 mousePosi;

	void Start () {
		
	}

	void Update () {
		mousePosi = Input.mousePosition;
		transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(mousePosi).x, 0, 0);
	}
}
