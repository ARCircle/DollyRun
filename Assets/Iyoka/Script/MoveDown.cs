using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
	Vector3 dir = new Vector3(0f, 0f, -5f);
	public bool suiside = false;
	
	// Update is called once per frame
	void Update () {
		if (!GrobalClass.gameover || !GrobalClass.pause) {
			this.transform.Translate (dir * Time.deltaTime, Space.World);
			if (suiside && transform.position.z < -100) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Reset () {
		if (!GrobalClass.gameover || !GrobalClass.pause) {
			this.transform.position = Vector3.zero;
		}
	}
}
