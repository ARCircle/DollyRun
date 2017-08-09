﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public GameObject[] prefabs;
    public Transform windowBelowTransform;
    public float itemSpeed;
    public GameObject player;
    public Transform destroyBorder;
    public float coinVaccumEndTime;
    public Transform vaccumBorder;

    //子オブジェクトのローカルポジションで親オブジェクトの下辺で指定できるようにする
    private BoxCollider prefabColl; //一番下のコライダー

    private List<GameObject> instanceList;
    private float coinVaccumEndTimer; //加算タイマー
    private float intervalTime; //生成するまでの時間
    private float instanceTimer; //加算タイマー
    private bool isVaccumed = false;

    // Use this for initialization
    void Start () {
        //ランダムに生成
        int first_selectPrefab = Random.Range(0, prefabs.Length);
        int second_selectPrefab = Random.Range(0, prefabs.Length);
        int third_selectPrefab = Random.Range(0, prefabs.Length);
        int fourth_selectPrefab = Random.Range(0, prefabs.Length);
        int fifth_selectPrefab = Random.Range(0, prefabs.Length);

        instanceList = new List<GameObject>();
        GameObject first = Instantiate(prefabs[first_selectPrefab]);
        GameObject second = Instantiate(prefabs[second_selectPrefab]);
        GameObject third = Instantiate(prefabs[third_selectPrefab]);
        //GameObject fourth = Instantiate(prefabs[fourth_selectPrefab]);
        //GameObject fifth = Instantiate(prefabs[fifth_selectPrefab]);
        first.transform.parent = this.transform;
        second.transform.parent = this.transform;
        third.transform.parent = this.transform;
        //fourth.transform.parent = this.transform;
        //fifth.transform.parent = this.transform;
        instanceList.Add(first);
        instanceList.Add(second);
        instanceList.Add(third);
        //instanceList.Add(fourth);
        //instanceList.Add(fifth);


        //それぞれの初期の場所指定
        //シーンにあるオブジェクトから初期の場所を指定する
        //他４つくらいも同じことをやる
        prefabColl = instanceList[0].GetComponent<BoxCollider>();
        BoxCollider instance2 = instanceList[1].GetComponent<BoxCollider>();
        BoxCollider instance3 = instanceList[2].GetComponent<BoxCollider>();
        //BoxCollider instance4 = instanceList[3].GetComponent<BoxCollider>();
        //BoxCollider instance5 = instanceList[4].GetComponent<BoxCollider>();
        instanceList[0].transform.position = windowBelowTransform.position +
            new Vector3(0, 0, prefabColl.bounds.extents.z) - prefabColl.bounds.center
           + prefabColl.transform.position;
        instanceList[1].transform.position = /*prefabColl.bounds.center + */
            new Vector3(windowBelowTransform.position.x, windowBelowTransform.position.y, prefabColl.bounds.center.z) +
            new Vector3(0, 0, prefabColl.bounds.extents.z) +
            new Vector3(0, 0, instance2.bounds.extents.z) - instance2.bounds.center
          + instance2.transform.position;
        instanceList[2].transform.position = /*instance2.bounds.center + */
            new Vector3(windowBelowTransform.position.x, windowBelowTransform.position.y, instance2.bounds.center.z) +
            new Vector3(0, 0, instance2.bounds.extents.z) +
            new Vector3(0, 0, instance3.bounds.extents.z) - instance3.bounds.center
            + instance3.transform.position;
        //instanceList[3].transform.position = /*instance3.bounds.center + */
        //    new Vector3(windowBelowTransform.position.x, windowBelowTransform.position.y, instance3.bounds.center.z) +
        //    new Vector3(0, 0, instance3.bounds.extents.z) +
        //    new Vector3(0, 0, instance4.bounds.extents.z) - instance4.bounds.center
        //   + instance4.transform.position;
        //instanceList[4].transform.position = /*instance4.bounds.center + */
        //    new Vector3(windowBelowTransform.position.x, windowBelowTransform.position.y, instance4.bounds.center.z) +
        //    new Vector3(0, 0, instance4.bounds.extents.z) +
        //    new Vector3(0, 0, instance5.bounds.extents.z) - instance5.bounds.center
        //    + instance5.transform.position;

        //アイテムの速度とプレファブの奥行きから、生成する時間間隔がわかる
        //その時間間隔で生成すればいい（アイテムは勝手に動いて勝手に消えてくれる）
        //リストの０のところにあるプレハブの奥行距離から生成間隔時間を毎回調べれば、
        //任意のバラバラの大きさのプレハブをまとめて自動生成できる
        intervalTime = prefabColl.bounds.size.z / itemSpeed;
        instanceTimer = 0;
        coinVaccumEndTimer = 0;

        //GrobalClass関連
        GrobalClass.coins = 0;
//        GrobalClass.itemRs = 0;
    }

    // Update is called once per frame
    void Update () {
//        Debug.Log("coin: " + GrobalClass.coins);
 //       Debug.Log("itemR: " + GrobalClass.itemRs);
        //下まで来たら削除して生成
        if (instanceTimer > intervalTime) {
            InstanceItem();

            //新しい間隔時間を設定
            prefabColl = instanceList[0].GetComponent<BoxCollider>();
            intervalTime = prefabColl.bounds.size.z / itemSpeed;
            instanceTimer = 0;
        }
        instanceTimer += Time.deltaTime;

        //アイテム使用ボタンが押されたら
        //if (GrobalClass.SetItemRUsed()) isItemUsed = true;
        if (Input.GetKeyDown("i") ) {
            GrobalClass.useItemR = true;            
        }
        //アイテムＲ使用中        
        if (GrobalClass.useItemR) {
            GrobalClass.useItemR = false;
            if (isVaccumed) //時間延長
                coinVaccumEndTimer -= coinVaccumEndTime;
            else
                isVaccumed = true;
        }
        if (isVaccumed) { 
            coinVaccumEndTimer += Time.deltaTime;
            if (coinVaccumEndTimer > coinVaccumEndTime) {
                coinVaccumEndTimer = 0;
                isVaccumed = false;
            }
        }
    }

    void InstanceItem() {
        //一番下のプレハブを削除
        GameObject destObj = instanceList[0];
        instanceList.Remove(destObj);
        Destroy(destObj);

        int selectPrefab = Random.Range(0, prefabs.Length);
        GameObject instancePrefab = Instantiate(prefabs[selectPrefab]);

        //生成する場所指定
        //positionは毎回可変するので調べる
        //(末尾のプレハブのmax.zから移動した分のprefabColl.bounds.size.zを引いて、
        //生成したプレハブのextents.zを足せばいい)
        //末尾のプレハブはlistObj[listObj.Count - 1]で取得できる
        BoxCollider backColl = instanceList[instanceList.Count - 1].GetComponent<BoxCollider>();
        BoxCollider newColl = instancePrefab.GetComponent<BoxCollider>();
        Vector3 newVec = instancePrefab.transform.position;
        newVec.x = windowBelowTransform.position.x - newColl.bounds.center.x;
        newVec.y = windowBelowTransform.position.y - newColl.bounds.center.y;
        newVec.z = backColl.bounds.max.z - prefabColl.bounds.size.z
            + newColl.bounds.extents.z - newColl.bounds.center.z;
        instancePrefab.transform.position = newVec + newColl.transform.position;

        instancePrefab.transform.parent = this.transform;
        instanceList.Add(instancePrefab);
    }

    public bool GetIsVaccumed() {
        return isVaccumed;
    }
}
