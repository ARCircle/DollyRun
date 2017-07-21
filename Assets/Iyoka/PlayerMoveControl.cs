using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour {
	public GameObject[] RailBase;
	GameObject body;
	GameObject railtip;
	Transform Temp;
	Vector3 mp, wp, pp, dir;
	Vector3 reset = new Vector3 (0f, -1f, 0f);
	//Vector3[,] Rails = new Vector3[3, 2000];
	GameObject[,] RailsObj = new GameObject[3, 2000];
	int[] RailsFin = new int[3];
	int touchcnt = 0, railcnt = 0;
	float touchtime = 0f, limit = 1f, dunit = 0.1f; 

	// Use this for initialization
	void Start () {
		railtip = (GameObject)Resources.Load ("Rail");
		RailBase [1].transform.Translate (0f, -0.4f, 0f);
		RailBase [2].transform.Translate (0f, -0.2f, 0f);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			touchtime = 0f;
		}
		if (Input.GetMouseButton (0) && touchtime < limit) {
			// ワールド座標の取得
			mp = Input.mousePosition;
			mp.z = 10f;
			pp = wp;
			wp = Camera.main.ScreenToWorldPoint(mp);
			// 点の補間
			if (touchcnt > 0) {
				if ((wp - pp).magnitude > 0.00001f) {
					dir = wp - pp;
					float delta = dir.magnitude;
					for (int i = 1; i < delta / dunit; i++) {
						PutRail (i, delta);
					}
					// 前の点との角度・方向計算
					TempLookAt (pp + dir);
					PutRail (0, 0f);
				}
			} else {
				PutRail (0, 0f);
			}
			touchtime += Time.deltaTime;
		} else if (touchcnt > 0) {
			//レール終端
			TempLookAt (wp + dir);
			wp = reset;
			pp = reset;
			dir = new Vector3 (0f, 0f, 0f);
			RailsFin[railcnt] = touchcnt;
			touchcnt = 0;
			railcnt = (railcnt + 1) % 3;
			RailBase [railcnt].transform.Translate (0f, 0.4f, 0f);
			RailBase [(railcnt + 1) % 3].transform.Translate (0f, -0.2f, 0f);
			RailBase [(railcnt + 2) % 3].transform.Translate (0f, -0.2f, 0f);
			for (int i = 0; i < 2000; i++) {
				Destroy(RailsObj [railcnt, i]);
			}
		}
	}

	void PutRail(int i, float delta){
		Vector3 vec;
		if (i > 0) {
			vec = pp + dir * dunit / delta * i;
			TempLookAt (vec);
		} else {
			vec = wp;
		}
		Temp = Instantiate (railtip).transform;
		Temp.position = vec - reset * 0.01f * touchcnt;
		Temp.SetParent (RailBase[railcnt].transform);
		//Rails [railcnt, touchcnt] = Temp.position;
		RailsObj [railcnt, touchcnt] = Temp.gameObject;
		touchcnt++;

	}

	void TempLookAt(Vector3 vec){
		Temp.LookAt (vec - reset * 0.01f * (touchcnt - 1));
	}

}
