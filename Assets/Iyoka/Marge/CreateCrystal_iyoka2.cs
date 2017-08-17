using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrystal_iyoka2 : MonoBehaviour {

	public GameObject prefab_A;
	public GameObject prefab_R;

	private int c;
	float time = 0f;

	void Start () {
		
	}

	void Update () {
		if (time <= 0f) {
			/*for (int i = 2; i < 20; i++) { 
				int type = Random.Range (0, 2);
				float x = Random.Range (-4.5f, 4.5f);
				if (type == 0) {
					GameObject crystal = Instantiate (prefab_A);
					crystal.transform.position = new Vector3 (x, 0, i * 5 + 20);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "A";
				} else {
					GameObject crystal = Instantiate (prefab_R);
					crystal.transform.position = new Vector3 (x, 0, i * 5 + 20);
					crystal.GetComponent<CrystalScript_iyoka> ().type = "R";
				}
			}*/
			int type = UnityEngine.Random.Range (0, 2);
			float x = UnityEngine.Random.Range (-4.5f, 4.5f);
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
