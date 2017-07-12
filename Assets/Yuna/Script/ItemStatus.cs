using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatus : MonoBehaviour {

	public int status_A;
	public int status_R;

	public GameObject prefab_A;
	public GameObject prefab_R;

	void Start () {
		
	}
	

	void Update () {
		
	}

	public void GetItem(Vector3 position, string type) {
		GameObject imageObj;
		if (type == "A") {
			imageObj = Instantiate (prefab_A);
			imageObj.GetComponent<RectTransform> ().anchoredPosition = Camera.main.WorldToScreenPoint (position);
		} else if (type == "R") {
			imageObj = Instantiate (prefab_R);
			imageObj.GetComponent<RectTransform> ().anchoredPosition = Camera.main.WorldToScreenPoint (position);
		}
	}
}
