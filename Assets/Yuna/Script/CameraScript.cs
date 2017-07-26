using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private GameObject trokko;

	void Start () {
		trokko = GameObject.Find ("Trokko");
	}

	void Update () {
		transform.position = new Vector3 (0, 8, trokko.transform.position.z - 4);
	}
}
