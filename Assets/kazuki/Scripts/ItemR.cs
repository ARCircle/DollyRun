using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemRの動きを設定するクラス
public class ItemR : MonoBehaviour {
    private float speed;
    private bool isPlayerTouched;

    // Use this for initialization
    void Start () {
        speed = transform.parent.GetComponent<ItemRManager>().GetSpeed();
        isPlayerTouched = false;		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= speed * transform.forward * Time.deltaTime;
	}

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Player")
            isPlayerTouched = true;
    }

    public bool GetIsPlayerTouched() {
        return isPlayerTouched;
    }
}
