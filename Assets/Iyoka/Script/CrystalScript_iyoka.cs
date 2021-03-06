﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript_iyoka : MonoBehaviour {

	private GameObject trokko;
	private ItemEffectControler_iyoka _itemEffectControler;
	public string type;

	void Start () {
		trokko = GameObject.Find ("Trokko");
		_itemEffectControler = GameObject.Find("GameManager").GetComponent<ItemEffectControler_iyoka> ();
	}

	void Update () {
		//衝突
		if (!GrobalClass.gameover && !GrobalClass.pause) {
			if(Vector3.Distance (transform.position, trokko.transform.position) < 1.6f) {
				_itemEffectControler.GetItem (type);
				Destroy (gameObject);
			}
		}
	}
}