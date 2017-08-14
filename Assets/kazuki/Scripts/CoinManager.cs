using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コインを生成・削除管理するクラス
public class CoinManager : MonoBehaviour {
    private GameObject player;
    private Transform destroyBorder;
    private Transform vacuumBorder;

    private List<Coin> instancedCoins; //生成されたアイテム達
    private float speed;
    private ItemManager itemMana;
    private bool preIsItemUsed;
    private float coinSpeedToPlayer = 15;

    // Use this for initialization
    void Start () {
        instancedCoins = new List<Coin>();
        Coin[] coins = GetComponentsInChildren<Coin>();
        for (int i = 0; i < coins.Length; i++)
            instancedCoins.Add(coins[i]);

        itemMana = GetComponentInParent<ItemManager>();
        player = itemMana.player;
        destroyBorder = itemMana.destroyBorder;
        vacuumBorder = itemMana.vaccumBorder;
        bool isVaccumed = itemMana.GetIsVaccumed();
        speed = itemMana.itemSpeed;

        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            instancedCoins[i].InitVacuum(vacuumBorder);
            instancedCoins[i].SetIsVacuumed(isVaccumed);
        }

        preIsItemUsed = false;
    }

    // Update is called once per frame
	void Update () {
		coinSpeedToPlayer = GrobalClass.speed * 3f;  // 勝手に追加してごめん
        for (int i = instancedCoins.Count - 1; i >= 0; i--) {
            //削除ポイントに到達するか、プレイヤーが触れたら
            if (instancedCoins[i].transform.position.z < destroyBorder.position.z ||
                instancedCoins[i].GetIsPlayerTouched()) {
                if (instancedCoins[i].GetIsPlayerTouched()) {
                    GrobalClass.coins++;
                    //player.GetComponent("PlayerController").getItem(coin.gameobject);
                }
                RemoveItem(instancedCoins[i]);
            }
        }

        //アイテムＲ使用ボタンがtrueになった瞬間
        if (GrobalClass.usingRtime > 0 && !preIsItemUsed)
            setIsVacuumedForCoins();
        //アイテム使用中
        if (itemMana.GetIsVaccumed()) {
            useItem();
        }
        //終わった瞬間
        if (!itemMana.GetIsVaccumed() && preIsItemUsed) {
            setNonIsVacuumedForCoins();
        }
        preIsItemUsed = itemMana.GetIsVaccumed();
    }

    private void useItem() {
        //画面内のコインを集める        
        foreach (Coin coin in instancedCoins) {
            if (coin.transform.position.z < vacuumBorder.position.z) {
                Vector3 directionToPlayer = (player.transform.position - coin.transform.position).normalized;
                coin.transform.position += directionToPlayer * coinSpeedToPlayer * Time.deltaTime;
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
