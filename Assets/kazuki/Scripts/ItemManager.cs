using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public GameObject[] prefabs;
    public Transform windowBelowTransform;
    public float itemSpeed;
    public GameObject player;
    public Transform destroyBorder;
    public Transform vaccumBorder;

    //子オブジェクトのローカルポジションで親オブジェクトの下辺で指定できるようにする
    private BoxCollider prefabColl; //一番下のコライダー

    private List<GameObject> instanceList;
    private float intervalTime; //生成するまでの時間
    private float instanceTimer; //加算タイマー
    public bool isVaccumed = false;

    // Use this for initialization
    void Start () {
        //ランダムに生成
        int first_selectPrefab = Random.Range(0, prefabs.Length - 1);
        int second_selectPrefab = Random.Range(0, prefabs.Length - 1);
        int third_selectPrefab = Random.Range(0, prefabs.Length - 1);
        int fourth_selectPrefab = Random.Range(0, prefabs.Length - 1);
        int fifth_selectPrefab = Random.Range(0, prefabs.Length - 1);

        instanceList = new List<GameObject>();
        GameObject first = Instantiate(prefabs[first_selectPrefab]);
        GameObject second = Instantiate(prefabs[second_selectPrefab]);
        GameObject third = Instantiate(prefabs[third_selectPrefab]);
        GameObject fourth = Instantiate(prefabs[fourth_selectPrefab]);
        GameObject fifth = Instantiate(prefabs[fifth_selectPrefab]);
        first.transform.parent = this.transform;
        second.transform.parent = this.transform;
        third.transform.parent = this.transform;
        fourth.transform.parent = this.transform;
        fifth.transform.parent = this.transform;
        instanceList.Add(first);
        instanceList.Add(second);
        instanceList.Add(third);
        instanceList.Add(fourth);
        instanceList.Add(fifth);


        //それぞれの初期の場所指定
        //シーンにあるオブジェクトから初期の場所を指定する
        //他４つくらいも同じことをやる
        prefabColl = instanceList[0].GetComponent<BoxCollider>();
        BoxCollider instance2 = instanceList[1].GetComponent<BoxCollider>();
        BoxCollider instance3 = instanceList[2].GetComponent<BoxCollider>();
        BoxCollider instance4 = instanceList[3].GetComponent<BoxCollider>();
        BoxCollider instance5 = instanceList[4].GetComponent<BoxCollider>();
        instanceList[0].transform.position = windowBelowTransform.position +
            new Vector3(0, 0, prefabColl.bounds.extents.z);
        instanceList[1].transform.position = prefabColl.bounds.center + 
            new Vector3(0, 0, prefabColl.bounds.extents.z) +
            new Vector3(0, 0, instance2.bounds.extents.z );
        instanceList[2].transform.position = instance2.bounds.center +
            new Vector3(0, 0, instance2.bounds.extents.z) +
            new Vector3(0, 0, instance3.bounds.extents.z);
        instanceList[3].transform.position = instance3.bounds.center +
            new Vector3(0, 0, instance3.bounds.extents.z) +
            new Vector3(0, 0, instance4.bounds.extents.z);
        instanceList[4].transform.position = instance4.bounds.center +
            new Vector3(0, 0, instance4.bounds.extents.z) +
            new Vector3(0, 0, instance5.bounds.extents.z);

        //アイテムの速度とプレファブの奥行きから、生成する時間間隔がわかる
        //その時間間隔で生成すればいい（アイテムは勝手に動いて勝手に消えてくれる）
        //リストの０のところにあるプレハブの奥行距離から生成間隔時間を毎回調べれば、
        //任意のバラバラの大きさのプレハブをまとめて自動生成できる
        intervalTime = prefabColl.bounds.size.z / itemSpeed;
        instanceTimer = 0;
    }

    // Update is called once per frame
    void Update () {
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
        //        if (isItemUsed()) isItemUsed = true;
        if (Input.GetKeyDown("i")) {
            isVaccumed = !isVaccumed;            
        }
    }

    void InstanceItem() {
        //一番下のプレハブを削除
        GameObject destObj = instanceList[0];
        instanceList.Remove(destObj);
        Destroy(destObj);

        int selectPrefab = Random.Range(0, prefabs.Length - 1);
        GameObject instancePrefab = Instantiate(prefabs[selectPrefab]);

        //生成する場所指定
        //positionは毎回可変するので調べる
        //(末尾のプレハブのmax.zから移動した分のprefabColl.bounds.size.zを引いて、
        //生成したプレハブのextents.zを足せばいい)
        //末尾のプレハブはlistObj[listObj.Count - 1]で取得できる
        BoxCollider backColl = instanceList[instanceList.Count - 1].GetComponent<BoxCollider>();
        BoxCollider newColl = instancePrefab.GetComponent<BoxCollider>();
        Vector3 newVec = instancePrefab.transform.position;
        //newVec.x = prefabColl.transform.position.x;
        //newVec.y = prefabColl.transform.position.y;
        //        newVec.z = backColl.bounds.max.z - prefabColl.bounds.size.z + newColl.bounds.extents.z;
        newVec = backColl.transform.position;
        instancePrefab.transform.position = newVec;

        instancePrefab.transform.parent = this.transform;
        instanceList.Add(instancePrefab);
    }
}
