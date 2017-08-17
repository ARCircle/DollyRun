using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コインの動きを設定するクラス
public class Coin : MonoBehaviour {
    private GameObject player;
	private float speed;
	private bool isPlayerTouched;
	private bool isVacuumed;
	private Transform vacuumBorder;
	private Vector3 angle;
    private float baseSize;
    private float extents; //あたり判定の大きさをコライダーに依存
	private AudioSource AS;
//    private float haloSize;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
		speed = transform.parent.GetComponent<CoinManager>().GetSpeed();
		AS = transform.parent.parent.GetComponent<AudioSource> ();
		isPlayerTouched = false;
		isVacuumed = false;
		angle = Vector3.zero;
//        transform.localScale = new Vector3(size, size, size);
	}

	//生成されたときにItemRの技を使用中かどうかをわかるようにするため
	public void InitVacuum(Transform vac) {
		vacuumBorder = vac;
	}

	// Update is called once per frame
	void Update() {
		speed = GrobalClass.speed;  // 勝手に追加してごめん
		if (!GrobalClass.gameover) {
            Vector3 planePlayer = player.transform.position;
            planePlayer.y = 0;
            Vector3 planeCoin = transform.position;
            planeCoin.y = 0;
            float disToPlayer = (planePlayer - planeCoin).magnitude;
            if (disToPlayer < extents) isPlayerTouched = true;
			transform.Rotate (new Vector3 (0, 6f, 0));
			//        Debug.Log(transform.position.z + " >" + vacuumBorder.position.z);
			//ItemRの技が使用中でなく、技の適用範囲に入っていなかったら
			if (!isVacuumed || transform.position.z > vacuumBorder.position.z)
				transform.position -= speed * Vector3.forward * Time.deltaTime;
		}
	}

	//void OnTriggerEnter(Collider coll) {
	//	if (coll.gameObject.tag == "Player") {
	//		isPlayerTouched = true;
	//	}
	//}

	public bool GetIsPlayerTouched() {
		if(isPlayerTouched) AS.PlayOneShot (AS.clip);
		return isPlayerTouched;
	}

	//ItemRの技が使用中かどうか設定してもらう
	public void SetIsVacuumed(bool b) {
		isVacuumed = b;
		//        Debug.Log(isVacuumed);
	}

    public void SetCoinSize(float baseS, float height) {
        transform.localScale = new Vector3(baseS, baseS, baseS);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        extents = GetComponent<SphereCollider>().bounds.extents.magnitude;
    }
}
