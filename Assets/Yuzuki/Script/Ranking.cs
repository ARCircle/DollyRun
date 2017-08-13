using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

	Text[] texts;

	// Use this for initialization
	void Start () {
		texts = GetComponentsInChildren <Text> ();
		for (int i = 0; i < texts.Length; i++) {
			if (texts [i].gameObject.name == "point") {
				//各ランキングのオブジェクトの番号を取得
				string name = texts [i].gameObject.transform.parent.name;
				name = name.Substring (4, 1);
				int num = System.Convert.ToInt32 (name);

				//textコンポーネントに値を代入
				texts[i].text = ScoreCalculator.TopScore[num - 1].ToString ();
			}				
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
