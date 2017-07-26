using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownInfinite : MonoBehaviour {
	public int team;
	const int teamnum = 7;
	Vector3 dir = new Vector3(0f, 0f, -15f);
	const float length = 35f;
	float unit = 0f;
	float timer = 0f;

	void Start () {
		unit = length / dir.magnitude;
		timer = unit * (team + 1);
		transform.position = transform.position - dir * unit * team;
	}

	// Update is called once per frame
	void Update () {
		this.transform.Translate (dir * Time.deltaTime);
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			transform.position = transform.position - dir * unit * teamnum;
			timer = unit * teamnum;
		}
	}
}
