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
		if (!GrobalClass.gameover && !GrobalClass.pause) {
			if (time <= 0f) {
				int type = Random.Range (0, 2);
				float x = Random.Range (-4.5f, 4.5f);
				if (type == 0) {
					GameObject crystal = Instantiate (prefab_A);
					crystal.transform.position = new Vector3 (x, 0, 40);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "A";
				} else {
					GameObject crystal = Instantiate (prefab_R);
					crystal.transform.position = new Vector3 (x, 0, 40);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "R";
				}
				time = 7 * 5f / GrobalClass.speed;
			} else {
				time -= Time.deltaTime;
			}
		}
	}
}
