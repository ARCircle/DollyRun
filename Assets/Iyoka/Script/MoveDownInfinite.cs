using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownInfinite : MonoBehaviour {
	public static bool enable = true;
	public int team = 0;
	public int teamnum = 7;
	public float length = 35f;
	Vector3 dir = new Vector3(0f, 0f, -5f);
	float unit = 0f;
	float timer = 0f;

	void Start () {
		for (int i = 1; i < teamnum; i++) {
			if (team == 0) {
				GameObject tmp = Instantiate<GameObject> (this.gameObject);
				tmp.transform.SetParent (this.transform.parent);
				tmp.GetComponent<MoveDownInfinite> ().team = i;
			} else {
				break;
			}
		}
		unit = length / dir.magnitude;
		timer = unit * (team + 1);
		transform.position = transform.position - dir * unit * team;
	}

	// Update is called once per frame
	void Update () {
		if (!GrobalClass.gameover) {
			this.transform.Translate (dir * Time.deltaTime);
			timer -= Time.deltaTime;
			if (timer <= 0f) {
				transform.position = transform.position - dir * unit * teamnum;
				timer = unit * teamnum;
			}
		}
	}
}
