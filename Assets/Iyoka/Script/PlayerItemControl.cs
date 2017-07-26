using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemControl : MonoBehaviour {
	const int N = 0, A = 1, R = 2, AR = 3;
	const int ItemMax = 3;
	int mode = N;
	int[] box = {0, 0};
	float[] usetime = {0f, 0f};

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		mode = 0;
		for (int i=0; i<2; i++) {
			if (usetime [i] > 0) {
				usetime [i] -= Time.deltaTime;
				mode += i;
			}
		}
	}

	public int Get(int item) {
		if(box [item - 1] < ItemMax) box [item - 1]++;
		return box [item - 1];
	}

	public int Num(int item) {
		return box [item - 1];
	}

	public void Use(int item) {
		//複数アイテム使用時の仕様によってコードが変わる
		for (int i=0; i<2; i++) {
			usetime [i] += (float)box [i];
			box [i] = 0;
		}
	}

	public int Mode() {
		return mode;
	}
}
