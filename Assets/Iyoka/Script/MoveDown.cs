using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
	public static bool enable = true;
	Vector3 dir = new Vector3(0f, 0f, -5f);
	public bool suiside = false;
	
	// Update is called once per frame
	void Update () {
		if (enable) {
			this.transform.Translate (dir * Time.deltaTime, Space.World);
			if (suiside && transform.position.z < -100) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Reset () {
		if (enable) {
			this.transform.position = Vector3.zero;
		}
	}
}
