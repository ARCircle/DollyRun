using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
	Vector3 dir = new Vector3(0f, 0f, -10f);
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (dir * Time.deltaTime);
	}

	public void Reset () {
		this.transform.position = Vector3.zero;
	}
}
