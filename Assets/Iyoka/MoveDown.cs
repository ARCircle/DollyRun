using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
	float speed = 0.3f;
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (0f, 0f, -speed);
	}

	public void Reset () {
		this.transform.position = Vector3.zero;
	}
}
