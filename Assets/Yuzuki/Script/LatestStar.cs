using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LatestStar : MonoBehaviour {

	public float flashSpeed = 24f;

	float alpha = 1;
	float speed = 0.04f;
	bool upAlpha = false;
	Text textImg;

	// Use this for initialization
	void Start () {
		if (ScoreCalculator.LatestScoreNum == -1) {
			this.gameObject.SetActive (false);
		} else {
			this.GetComponent <RectTransform> ().localPosition = new Vector3 (-270, 320 - ScoreCalculator.LatestScoreNum * 110, 0);
			string name = "rank" + (ScoreCalculator.LatestScoreNum + 1);
			textImg = GameObject.Find("ranking").transform.Find (name).Find ("point").gameObject.GetComponent <Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, 0, 5);

		//アクティブ時
		if (upAlpha) {
			alpha += speed * flashSpeed;
			if (alpha > 1) upAlpha = false;
		} else {
			alpha -= speed * flashSpeed;
			if (alpha < 0)	upAlpha = true;
		}
		textImg.color = new Color (0.75f, 0.75f, 0f, alpha);
	}
}
