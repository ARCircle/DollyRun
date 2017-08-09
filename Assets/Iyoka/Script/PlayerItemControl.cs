using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemControl : MonoBehaviour {
	GameObject Body;
	//BoxCollider bc;
	public GameObject auraA;
	GameObject auraAinst;
	bool usingA = false;

	void Start() {
		Body = transform.Find ("Trokko").gameObject;
		//bc = Body.GetComponent<BoxCollider> ();
	}

	void Update () {
		if (GrobalClass.usingAtime > 0f) {
			if (usingA == false) {
				auraAinst = Instantiate<GameObject> (auraA);
				auraAinst.transform.SetParent (Body.transform);
				auraAinst.transform.localPosition = new Vector3(0f, 0.35f, -0.59f);
				auraAinst.transform.Rotate (30f, 0f, 0f);
				usingA = true;
				Debug.Log ("active:" + auraAinst.name);
				//bc.enabled = false;
			}
			GrobalClass.usingAtime -= Time.deltaTime;
		} else if(usingA == true){
			//bc.enabled = true;
			usingA = false;
			Destroy (auraAinst);
		}
		if (GrobalClass.usingRtime > 0f) {
			GrobalClass.usingRtime -= Time.deltaTime;
		}
	}

	/*const int N = 0, A = 1, R = 2, AR = 3;
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
	*/
}
