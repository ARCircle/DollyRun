using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {
	private float Time = 0;
	private const float span = 10f;
	public GameObject block0;
	public GameObject block1;
	public GameObject block2;
	public GameObject block3;
	public GameObject block4;
	public GameObject block5;
	public GameObject block6;
	public GameObject block7;
	public GameObject block8;
	public GameObject block9;
	public GameObject block10;
	public GameObject block11;
	public GameObject block12;

	private int obj1;
	private int obj2;
	private int obj3;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (StageActive.isTrue ()) {

			Vector3 pos = transform.position;

			Time -= UnityEngine.Time.deltaTime * GrobalClass.speed / 5f;

			if (Time <= 0.0) {
				Time = 8.0f;
				if (GrobalClass.distance < 200) {
					obj1 = UnityEngine.Random.Range (1, 12);
					obj2 = 0;
					obj3 = 0;
				} else if ((GrobalClass.distance >= 200) && (GrobalClass.distance < 500)) {
					obj1 = UnityEngine.Random.Range (1, 12);
					obj2 = UnityEngine.Random.Range (1, 12);
					obj3 = 0;
				} else {
					obj1 = UnityEngine.Random.Range (0, 12);
					obj2 = UnityEngine.Random.Range (0, 12);
					obj3 = UnityEngine.Random.Range (0, 12);
				}


				for (int i = 0; i < 3; i++) {
					int objnum = 0;
					switch (i) {
					case 0:
						objnum = obj1;
						break;
					case 1:
						objnum = obj2;
						break;
					case 2:
						objnum = obj3;
						break;
					}
					if (objnum == 0) {
						block0.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block0);
					}
					if (objnum == 1) {
						block1.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block1);
					}
					if (objnum == 2) {
						block2.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block2);
					}
					if (objnum == 3) {
						block3.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block3);
					}
					if (objnum == 4) {
						block4.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block4);
					}
					if (objnum == 5) {
						block5.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block5);
					}
					if (objnum == 6) {
						block6.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block6);
					} 
					if (objnum == 7) {
						block7.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block7);
					}
					if (objnum == 8) {
						block8.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block8);
					}
					if (objnum == 9) {
						block9.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block9);
					}
					if (objnum == 10) {
						block10.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block10);
					}
					if (objnum == 11) {
						block11.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block11);
					}
					if (objnum == 12) {
						block12.transform.position = pos + Vector3.forward * i * span;
						UnityEngine.Object.Instantiate (this.block12);
					}
				}
				//Debug.Log (obj1);
				//Debug.Log (obj2);
				//Debug.Log (obj3);
			}
		}
	}
}
