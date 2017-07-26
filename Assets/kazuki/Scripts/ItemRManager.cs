using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemRを生成・削除管理するクラス
public class ItemRManager : MonoBehaviour {
    private GameObject player;
    public GameObject itemRPrefab;
    private Transform destroyBorder; //削除する境界線
    private Transform coinVacuumBorder; //コインがプレイヤーに吸い寄せられる境界線
    private float afterUsedItemTimer; //アイテム使用の終了時間

    private float coinSpeedToPlayer; //集まる際のコインの速度
    private List<ItemR> instancedItemRs; //生成されたアイテム達
    private float speed;
    private CoinManager coinManager; //コインを集める技を使うため
    private bool isItemUsed; //アイテム使用ボタンが押されたかどうか
    private float usedItemTimer; //アイテムを使用している間のタイマー

    // Use this for initialization
    void Start() {
        coinSpeedToPlayer = 15;
        coinManager = GetComponent<CoinManager>();
        instancedItemRs     = new List<ItemR>();
        ItemR[] itemRs = GetComponentsInChildren<ItemR>();
        for (int i = 0; i < itemRs.Length; i++)
            instancedItemRs.Add(itemRs[i]);

        isItemUsed = false;
        usedItemTimer = 0;

        ItemManager itemManager = GetComponentInParent<ItemManager>();
        player = itemManager.player;
        destroyBorder = itemManager.destroyBorder;
        afterUsedItemTimer = itemManager.coinVaccumEndTime;
        coinVacuumBorder = itemManager.vaccumBorder;
        speed = itemManager.itemSpeed;
    }

    // Update is called once per frame
    void Update() {
        //foreachのなかでlistを変更すると例外がでるから
        for (int i = instancedItemRs.Count - 1; i >= 0; i--) {
            //削除ポイントに到達するか、プレイヤーが触れたら
            if (instancedItemRs[i].transform.position.z < destroyBorder.position.z ||
                instancedItemRs[i].GetIsPlayerTouched()) {
                if (instancedItemRs[i].GetIsPlayerTouched()) {
                    //player.GetComponent("PlayerController").getItem(coin.gameobject);
                }
                RemoveItem(instancedItemRs[i]);
            }
        }

        //アイテム使用ボタンが押されたら
        //        if (isItemUsed()) isItemUsed = true;
        if (Input.GetKeyDown("i")) {
            isItemUsed = true;
            coinManager.setIsVacuumedForCoins();
        }
        //アイテム使用中
        if (isItemUsed) {
            useItem();
            usedItemTimer += Time.deltaTime;
            //終了時間に到達したら
            if (usedItemTimer > afterUsedItemTimer) {
                isItemUsed = false;
                usedItemTimer = 0;
                coinManager.setNonIsVacuumedForCoins();
            }
        }
    }

    private void useItem() {
        //画面内のコインを集める        
        List<Coin> coins = coinManager.GetCoins();
        foreach (Coin coin in coins) {
            if (coin.transform.position.z < coinVacuumBorder.position.z) {
                Vector3 directionToPlayer = (player.transform.position - coin.transform.position).normalized;
                coin.transform.position += directionToPlayer * coinSpeedToPlayer * Time.deltaTime;
            }
        }
    }

    //ItemRを削除する
    private void RemoveItem(ItemR itemR) {
        instancedItemRs.Remove(itemR);
        Destroy(itemR.gameObject);
    }

    public float GetSpeed() {
        return speed;
    }
}
