using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrystal_iyoka : MonoBehaviour {

	public GameObject prefab_A;
	public GameObject prefab_R;

	private int c;
	float time = 0f;

	void Start () {
		
	}

	void Update () {
		if (StageActive.isTrue()) {
			if (time <= 0f) {
				int type = Random.Range (0, 2);
				float x = -4.5f + (Random.Range (1, 6) - 1) * 2.25f;
				if (type == 0) {
					GameObject crystal = Instantiate (prefab_A);
					crystal.transform.position = new Vector3 (x, 0, 40);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "A";
				} else {
					GameObject crystal = Instantiate (prefab_R);
					crystal.transform.position = new Vector3 (x, 0, 40);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "R";
				}
				time = 7f * (1.5f + 5f / GrobalClass.speed) / 2.5f;
			} else {
				time -= Time.deltaTime;
			}
		}
	}
}
