using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コインを生成・削除管理するクラス
public class OldCoinManager : MonoBehaviour {
    public GameObject player;
    public GameObject coinPrefab;
    public Transform generateBorderLeft;
    public Transform generateBorderRight;
    public Transform destroyBorder;
    public Transform vacuumBorder;
    public float intervalMinTime;
    public float intervalMaxTime;

    private List<Coin> instancedCoins; //生成されたアイテム達
    private float instanceTimer; //生成するタイマー
    private float afterInstanceTime; //どの間隔で生成するか
    private bool isVacuumedForCoins;

    // Use this for initialization
    void Start() {
        instancedCoins = new List<Coin>();
        instanceTimer = 0;
        afterInstanceTime = Random.Range(intervalMinTime, intervalMaxTime);
        isVacuumedForCoins = false;

        //ItemManager itemManager = GetComponentInParent<ItemManager>();
        //player = itemManager.player;
        //destroyBorder = itemManager.destroyBorder;
        //vacuumBorder = itemManager.vaccumBorder;
        //bool isVaccumed = itemManager.isVaccumed;
        //for (int i = instancedCoins.Count - 1; i >= 0; i--) {
        //    instancedCoins[i].InitVacuum(vacuumBorder);
        //    instancedCoins[i].SetIsVacuumed(isVaccumed);
        //}
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

        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            //削除ポイントに到達するか、プレイヤーが触れたら
            if (instancedCoins[i].transform.position.z < destroyBorder.position.z ||
                instancedCoins[i].GetIsPlayerTouched()) {
                if (instancedCoins[i].GetIsPlayerTouched()) {
                    //player.GetComponent("PlayerController").getItem(coin.gameobject);
                }
                RemoveItem(instancedCoins[i]);
            }
        }
    }

    //itemRが使用された瞬間に呼ばれる
    public void setIsVacuumedForCoins() {
        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            instancedCoins[i].SetIsVacuumed(true);
        }
        isVacuumedForCoins = true;
    }

    //ItemRが終わる瞬間に呼ばれる。
    public void setNonIsVacuumedForCoins() {
        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            instancedCoins[i].SetIsVacuumed(false);
        }
        isVacuumedForCoins = false;
    }

    //コインを削除する
    private void RemoveItem(Coin coin) {
        instancedCoins.Remove(coin);
        Destroy(coin.gameObject);
    }

    //コインをランダムな場所に生成する
    private void GenerateItem() {
        float generatePosX = Random.Range(generateBorderLeft.transform.position.x,
                                                                 generateBorderRight.transform.position.x);
        Vector3 generatePos = new Vector3(generatePosX,
                                                               generateBorderLeft.transform.position.y,
                                                               generateBorderLeft.transform.position.z);

        GameObject coin = Instantiate(coinPrefab, generatePos, Quaternion.identity);
        Coin coinScript = coin.GetComponent<Coin>();
        instancedCoins.Add(coinScript);
        coin.transform.parent = this.transform;
        coinScript.InitVacuum(vacuumBorder);
        coinScript.SetIsVacuumed(isVacuumedForCoins);
    }

    public List<Coin> GetCoins() {
        return instancedCoins;
    }
}
