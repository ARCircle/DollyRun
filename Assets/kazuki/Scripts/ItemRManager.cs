using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemRを生成・削除管理するクラス
public class ItemRManager : MonoBehaviour {
    private GameObject player;
//    public GameObject itemRPrefab;
    private Transform destroyBorder; //削除する境界線
//    private Transform coinVacuumBorder; //コインがプレイヤーに吸い寄せられる境界線
    private float afterUsedItemTimer; //アイテム使用の終了時間

//    private float coinSpeedToPlayer; //集まる際のコインの速度
    private List<ItemR> instancedItemRs; //生成されたアイテム達
    private float speed;
    private CoinManager coinManager; //コインを集める技を使うため
    private bool preIsItemUsed; //前フレームにアイテム使用ボタンが押されたかどうか
    private ItemManager itemMana;

    // Use this for initialization
    void Start() {
 //       coinSpeedToPlayer = 15;
        coinManager = GetComponent<CoinManager>();
        instancedItemRs     = new List<ItemR>();
        ItemR[] itemRs = GetComponentsInChildren<ItemR>();
        for (int i = 0; i < itemRs.Length; i++)
            instancedItemRs.Add(itemRs[i]);

//        isItemUsed = false;
//        usedItemTimer = 0;

        itemMana = GetComponentInParent<ItemManager>();
        player = itemMana.player;
        destroyBorder = itemMana.destroyBorder;
        afterUsedItemTimer = itemMana.coinVaccumEndTime;
//        coinVacuumBorder = itemMana.vaccumBorder;
        speed = itemMana.itemSpeed;
    }

    // Update is called once per frame
    void Update() {
        //foreachのなかでlistを変更すると例外がでるから
        for (int i = instancedItemRs.Count - 1; i >= 0; i--) {
            //削除ポイントに到達するか、プレイヤーが触れたら
            if (instancedItemRs[i].transform.position.z < destroyBorder.position.z ||
                instancedItemRs[i].GetIsPlayerTouched()) {
                if (instancedItemRs[i].GetIsPlayerTouched()) {
//                    GrobalClass.itemRs++;
                    //player.GetComponent("PlayerController").getItem(coin.gameobject);
                }
                RemoveItem(instancedItemRs[i]);
            }
        }

        ////アイテムＲ使用ボタンがtrueになった瞬間
        //if (itemMana.GetIsVaccumed() && !preIsItemUsed)
        //    coinManager.setIsVacuumedForCoins();
        ////アイテム使用中
        //if (itemMana.GetIsVaccumed()) {
        //    useItem();
        //}
        ////終わった瞬間
        //if (!itemMana.GetIsVaccumed() && preIsItemUsed) {
        //    coinManager.setNonIsVacuumedForCoins();
        //}
        //preIsItemUsed = itemMana.GetIsVaccumed();
    }

    //private void useItem() {
    //    //画面内のコインを集める        
    //    List<Coin> coins = coinManager.GetCoins();
    //    foreach (Coin coin in coins) {
    //        if (coin.transform.position.z < coinVacuumBorder.position.z) {
    //            Vector3 directionToPlayer = (player.transform.position - coin.transform.position).normalized;
    //            coin.transform.position += directionToPlayer * coinSpeedToPlayer * Time.deltaTime;
    //        }
    //    }
    //}

    //ItemRを削除する
    private void RemoveItem(ItemR itemR) {
        instancedItemRs.Remove(itemR);
        Destroy(itemR.gameObject);
    }

    public float GetSpeed() {
        return speed;
    }
}
