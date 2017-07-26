using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コインを生成・削除管理するクラス
public class CoinManager : MonoBehaviour {
    private GameObject player;
    public GameObject coinPrefab;
    private Transform destroyBorder;
    private Transform vacuumBorder;

    private List<Coin> instancedCoins; //生成されたアイテム達
    private float speed;

    // Use this for initialization
    void Start () {
        instancedCoins = new List<Coin>();
        Coin[] coins = GetComponentsInChildren<Coin>();
        for (int i = 0; i < coins.Length; i++)
            instancedCoins.Add(coins[i]);

        ItemManager itemManager = GetComponentInParent<ItemManager>();
        player = itemManager.player;
        destroyBorder = itemManager.destroyBorder;
        vacuumBorder = itemManager.vaccumBorder;
        bool isVaccumed = itemManager.isVaccumed;
        speed = itemManager.itemSpeed;

        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            instancedCoins[i].InitVacuum(vacuumBorder);
            instancedCoins[i].SetIsVacuumed(isVaccumed);
        }
    }

    // Update is called once per frame
    void Update () {
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
    }

    //ItemRが終わる瞬間に呼ばれる。
    public void setNonIsVacuumedForCoins() {
        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            instancedCoins[i].SetIsVacuumed(false);
        }
    }

    //コインを削除する
    private void RemoveItem(Coin coin) {
        instancedCoins.Remove(coin);
        Destroy(coin.gameObject);
    }

    public float GetSpeed() {
        return speed;
    }

    public List<Coin> GetCoins() {
        return instancedCoins;
    }
}
