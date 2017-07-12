using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour {
	GameObject body;
	GameObject railtip;
	Vector3 mp, wp;
	Vector3 reset = new Vector3 (0f, -1f, 0f);
	Vector3[,] Rails = new Vector3[20, 600];
	int touchcnt = 0, railcnt = 0;
	float touchtime = 0f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) && touchtime < 5f) {
			mp = Input.mousePosition;
			mp.z = 10f;
			wp = Camera.main.ScreenToWorldPoint(mp);
			Rails [railcnt, touchcnt] = wp;
			Instantiate (railtip).transform.position = wp;
			touchtime += Time.deltaTime;
			touchcnt++;
		} else if (touchcnt > 0) {
			touchcnt = 0;
			touchtime = 0f;
			railcnt = (railcnt + 1) % 20;
			for (int i = 0; i < 600; i++) {
				Rails [railcnt, i] = reset;
			}
		}
	}

}
