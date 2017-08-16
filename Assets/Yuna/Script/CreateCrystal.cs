using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrystal : MonoBehaviour {

	public GameObject prefab_A;
	public GameObject prefab_R;

	private int c;

	void Start () {
		for (int i = 2; i < 20; i++) { 
			int type = UnityEngine.Random.Range (0, 2);
			float x = UnityEngine.Random.Range (-2.0f, 2.0f);
			if (type == 0) {
				GameObject crystal = Instantiate (prefab_A);
				crystal.transform.position = new Vector3 (x, 0, i * 5);
				crystal.GetComponent<CrystalScript> ().type = "A";
			} else {
				GameObject crystal = Instantiate (prefab_R);
				crystal.transform.position = new Vector3 (x, 0, i * 5);
				crystal.GetComponent<CrystalScript> ().type = "R";
			}
		}
	}

	void Update () {
		
	}
}
