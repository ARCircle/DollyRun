using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchStart : MonoBehaviour {

	public float flashSpeed = 1.2f;

	float alpha = 1;
	float speed = 0.04f;
	bool upAlpha = false;
	bool active = true;
	Text textImg;
	GameObject touch_start;
	Animation CameraMove;

	Fader fade = new Fader ();


	// Use this for initialization
	void Start () {
		textImg = GetComponent <Text> ();
		touch_start = transform.parent.Find ("button").gameObject;
		CameraMove = GameObject.Find ("Camera").GetComponent<Animation> ();

		StartCoroutine (fade.blackout (1.0f, DeletePanel));
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			//アクティブ時
			if (upAlpha) {
				alpha += speed * flashSpeed;
				if (alpha > 1) upAlpha = false;
			} else {
				alpha -= speed * flashSpeed;
				if (alpha < 0)	upAlpha = true;
			}
			//透過率更新
			textImg.color = new Color (textImg.color.r, textImg.color.g, textImg.color.b, alpha);
		
		} else {
			//ノンアクティブ時
			alpha -= speed * flashSpeed;
			textImg.color = new Color (textImg.color.r, textImg.color.g, textImg.color.b, alpha);
			if (alpha < 0) {

				Image[] imgs = GameObject.Find ("StartScene").GetComponentsInChildren <Image> ();
				StartCoroutine(fade.fadein (1.0f, imgs, DisenableThisObj));
				//StartCoroutine(GameObject.Find("MainCanvas").GetComponent <Fader> ().fadein (1.0f, imgs, DisenableThisObj));
			}
		}

	}

	void DisenableThisObj () {
		transform.parent.gameObject.SetActive (false);
	}

	public void DoNonactive () {
		active = false;
		upAlpha = true;
		//タッチスタートボタンを無効化	
		touch_start.SetActive (false);
		//カメラの移動
		CameraMove.Play ();
	}

	void DeletePanel () {
		Destroy (GameObject.Find ("BlackPlate(Clone)"));
	}
		




	//使わないが、残しておく
	//TouchScreenを入力する前の状態に移行
	/*
	public void DoActive () {
		active = true;
		upAlpha = true;
		alpha = 0;
		//タッチスタートボタンを有効化
		touch_start.SetActive (true);
	}
	*/

}
