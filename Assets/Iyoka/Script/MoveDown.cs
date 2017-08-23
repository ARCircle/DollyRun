using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour {
	Vector3 startpos;
	Vector3 dir = new Vector3(0f, 0f, -1f);
	public bool suiside = false;

	void Start () {
		startpos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (StageActive.isTrue()) {
			this.transform.Translate (dir * Time.deltaTime * GrobalClass.speed, Space.World);
			if (suiside && transform.position.z < -10) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Reset () {
		if (StageActive.isTrue()) {
			this.transform.position = startpos;
		}
	}
}
