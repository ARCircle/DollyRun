using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour {

	private GameObject trokko;
	private ItemEffectControler _itemEffectControler;
	public string type;

	void Start () {
		trokko = GameObject.Find ("Trokko");
		_itemEffectControler = GameObject.Find("GameManager").GetComponent<ItemEffectControler> ();
	}

	void Update () {
		//衝突
		if (Vector3.Distance (transform.position, trokko.transform.position) < 0.8f) {
			_itemEffectControler.GetItem (type);
			Destroy (gameObject);
		}
	}
}
