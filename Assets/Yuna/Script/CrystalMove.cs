using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMove : MonoBehaviour {

	private GameObject trokko;
	private ItemStatus _itemStatus;
	public string type;

	void Start () {
		trokko = GameObject.Find ("Trokko");
		_itemStatus = GetComponent<ItemStatus> ();
	}

	void Update () {
		transform.Translate(0, 0, -0.2f);

		//衝突
		if (Vector3.Distance (transform.position, trokko.transform.position) < 0.8f) {
			_itemStatus.GetItem (transform.position, type);
			Destroy (gameObject);
		}

		//画面外除去
		if (transform.position.z < -5) {
			Destroy (gameObject);
		}
	}
}
