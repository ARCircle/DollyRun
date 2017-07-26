using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コインの動きを設定するクラス
public class Coin : MonoBehaviour {
    private float speed;
    private bool isPlayerTouched;
    private bool isVacuumed;
    private Transform vacuumBorder;

    // Use this for initialization
    void Start() {
        speed = transform.parent.GetComponent<CoinManager>().GetSpeed();
        isPlayerTouched = false;
        isVacuumed = false;
    }

    //生成されたときにItemRの技を使用中かどうかをわかるようにするため
    public void InitVacuum(Transform vac) {
        vacuumBorder = vac;
    }

    // Update is called once per frame
    void Update() {
//        Debug.Log(transform.position.z + " >" + vacuumBorder.position.z);
//ItemRの技が使用中でなく、技の適用範囲に入っていなかったら
        if (!isVacuumed || transform.position.z > vacuumBorder.position.z)
            transform.position -= speed * transform.forward * Time.deltaTime;
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.tag == "Player") {
            isPlayerTouched = true;
        }
    }

    public bool GetIsPlayerTouched() {
        return isPlayerTouched;
    }

    //ItemRの技が使用中かどうか設定してもらう
    public void SetIsVacuumed(bool b) {
        isVacuumed = b;
//        Debug.Log(isVacuumed);
    }
}
