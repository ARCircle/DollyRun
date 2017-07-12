using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrystal : MonoBehaviour {

	public GameObject prefab_A;
	public GameObject prefab_R;

	private int c;

	void Start () {
		
	}

	void Update () {
		c++;
		if (c % 60 == 0) {
			int type = Random.Range (0, 2);
			float x = Random.Range (-2.0f, 1.8f);
			if (type == 0) {
				GameObject crystal = Instantiate (prefab_A, new Vector3(x, -0.5f, 10), Quaternion.identity);
				crystal.GetComponent<CrystalMove> ().type = "A";
			} else {
				GameObject crystal = Instantiate (prefab_R, new Vector3(x, -0.5f, 10), Quaternion.identity);
				crystal.GetComponent<CrystalMove> ().type = "R";
			}
		}
	}
}
