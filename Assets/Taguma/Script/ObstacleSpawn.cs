﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {
	private float Time = 0;
	private const float span = 6f;
	public GameObject block0;
	public GameObject block1;
	public GameObject block2;
	public GameObject block3;
	public GameObject block4;
	public GameObject block5;
	public GameObject block6;

	private int obj1;
	private int obj2;
	private int obj3;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.position;

		Time -= UnityEngine.Time.deltaTime * GrobalClass.speed / 5f;

		if (Time <= 0.0) {
			Time = 8.0f;
			while (true) {
				obj1 = UnityEngine.Random.Range (6, 7);
				obj2 = UnityEngine.Random.Range (6, 7);
				obj3 = UnityEngine.Random.Range (6, 7);

				if ((obj1 == 1 || obj2 == 1 || obj3 == 1) && (((obj1 == 2) || (obj2 == 2) || (obj3 == 2))|| (obj1 == 4) || (obj2 == 4) || (obj3 == 4))) {

				} else if ((obj1 == 2 || obj2 == 2 || obj3 == 2) && (((obj1 == 1) || (obj2 == 1) || (obj3 == 1)) || ((obj1 == 4) || (obj2 == 4) || (obj3 == 4)) || ((obj1 == 5) || (obj2 == 5) || (obj3 == 5)))) {

				} else if ((obj1 == 3 || obj2 == 3 || obj3 == 3) && (((obj1 == 4) || (obj2 == 4) || (obj3 == 4)) || ((obj1 == 5) || (obj2 == 5) || (obj3 == 5)) || ((obj1 == 6) || (obj2 == 6) || (obj3 == 6)))) {

				} else if ((obj1 == 4 || obj2 == 4 || obj3 == 4) && (((obj1 == 1) || (obj2 == 1) || (obj3 == 1)) || ((obj1 == 3) || (obj2 == 3) || (obj3 == 3)) || ((obj1 == 5) || (obj2 == 5) || (obj3 == 5)) || ((obj1 == 6) || (obj2 == 6) || (obj3 == 6)))) {

				} else if ((obj1 == 5 || obj2 == 5 || obj3 == 5) && (((obj1 == 3) || (obj2 == 3) || (obj3 == 3)) || ((obj1 == 4) || (obj2 == 4) || (obj3 == 4)) || ((obj1 == 6) || (obj2 == 6) || (obj3 == 6)))) {
			
				} else if ((obj1 == 6 || obj2 == 6 || obj3 == 6) && (((obj1 == 3) || (obj2 == 3) || (obj3 == 3)) || ((obj1 == 4) || (obj2 == 4) || (obj3 == 4)))) {
					
				} else {
					break;
				}

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
					UnityEngine.Object.Instantiate (this.block0);
					block0.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 1) {
					UnityEngine.Object.Instantiate (this.block1);
					block1.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 2) {
					UnityEngine.Object.Instantiate (this.block2);
					block2.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 3) {
					UnityEngine.Object.Instantiate (this.block3);
					block3.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 4) {
					UnityEngine.Object.Instantiate (this.block4);
					block4.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 5) {
					UnityEngine.Object.Instantiate (this.block5);
					block5.transform.position = pos + Vector3.forward * i * span;
				}
				if (objnum == 6) {
					UnityEngine.Object.Instantiate (this.block6);
					block6.transform.position = pos + Vector3.forward * i * span;
				} 

				//Debug.Log (obj1);
				//Debug.Log (obj2);
				//Debug.Log (obj3);
			}
		}
	}
}
