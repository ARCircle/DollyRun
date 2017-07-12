using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemRを生成・削除管理するクラス
public class ItemRManager : MonoBehaviour {
    public GameObject player;
    public GameObject itemRPrefab;
    public Transform generateBorderLeft; //生成する境界線の最左
    public Transform generateBorderRight; //生成する境界線の最右
    public Transform destroyBorder; //削除する境界線
    public Transform coinVacuumBorder; //コインがプレイヤーに吸い寄せられる境界線
    public float intervalMinTime;
    public float intervalMaxTime;
    public float afterUsedItemTimer; //アイテム使用の終了時間
    public float coinSpeedToPlayer; //集まる際のコインの速度

    private CoinManager coinManager; //コインを集める技を使うため
    private List<ItemR> instancedItemRs; //生成されたアイテム達
    private bool isItemUsed; //アイテム使用ボタンが押されたかどうか
    private float instanceTimer; //生成するタイマー
    private float afterInstanceTime; //どの間隔で生成するか
    private float usedItemTimer; //アイテムを使用している間のタイマー

    // Use this for initialization
    void Start() {
        coinManager = GetComponent<CoinManager>();
        instancedItemRs     = new List<ItemR>();
        isItemUsed = false;
        instanceTimer = 0;
        afterInstanceTime = Random.Range(intervalMinTime, intervalMaxTime);
        usedItemTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        //ランダム時間後にアイテム生成
        instanceTimer += Time.deltaTime;        
        if (instanceTimer > afterInstanceTime) {
            instanceTimer = 0;
            afterInstanceTime = Random.Range(intervalMinTime, intervalMaxTime);
            GenerateItem();
        }

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

    //ItemRをランダムな場所に生成する
    private void GenerateItem() {
        float generatePosX = Random.Range(generateBorderLeft.transform.position.x, 
                                                                 generateBorderRight.transform.position.x);
        Vector3 generatePos = new Vector3(generatePosX,
                                                               generateBorderLeft.transform.position.y,
                                                               generateBorderLeft.transform.position.z);

        GameObject itemR = Instantiate(itemRPrefab, generatePos, Quaternion.identity);
        itemR.transform.parent = this.transform;
        instancedItemRs.Add(itemR.GetComponent<ItemR>());
    }
}
