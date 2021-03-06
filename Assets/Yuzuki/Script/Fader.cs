﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour{

	//使い方
	//Faderのインスタンスを生成した後、使いたいメソッドを使う
	//targetImgはfadeさせたいImageコンポーネントの配列
	//TargetFunc funcには、fadeの終了後に使いたい関数を入れる
	//ただし、入れられるのは、返り値も引数も無い関数のみ
	//関数名のみを引数に入れるだけでよい、()の部分はいらない
	//関数は引数に入れなくてもよい。その場合、fade後には何も実行されない

	public delegate void TargetFunc ();

	public IEnumerator fadein (float ProcessingTime, Image[] targetImg, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;

		//フェードイン
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = time / ProcessingTime;
			for (int j = 0; j < targetImg.Length; j++) {
				targetImg [j].color = new Color (targetImg [j].color.r, targetImg [j].color.g, targetImg [j].color.b, alpha);
			}

			yield return null;
		}
		//フェードイン終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}


	public IEnumerator fadeout (float ProcessingTime, Image[] targetImg, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;

		//フェードアウト
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = 1 - time / ProcessingTime;
			for (int j = 0; j < targetImg.Length; j++) {
				targetImg [j].color = new Color (targetImg [j].color.r, targetImg [j].color.g, targetImg [j].color.b, alpha);
			}

			yield return null;
		}
		//フェードアウト終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}


	public IEnumerator blackin (float ProcessingTime, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;
		Image img = Instantiate (Resources.Load ("BlackPlate") as GameObject).transform.Find ("Image").gameObject.GetComponent <Image> ();

		//フェードイン
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = time / ProcessingTime;
			img.color = new Color (0, 0, 0, alpha);

			yield return null;
		}
		//フェードイン終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}


	public IEnumerator blackout (float ProcessingTime, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;
		Image img = Instantiate (Resources.Load ("BlackPlate") as GameObject).transform.Find ("Image").gameObject.GetComponent <Image> ();

		//フェードイン
		while (time < ProcessingTime) {
			yield return null;
			time += Time.deltaTime;
			alpha = 1 - time / ProcessingTime;
			img.color = new Color (0, 0, 0, alpha);

			//yield return null;
		}
		//フェードイン終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}



	public IEnumerator fadein2 (float ProcessingTime, Image[] targetImg, Text[] targetText, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;

		//フェードイン
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = time / ProcessingTime;
			for (int j = 0; j < targetImg.Length; j++) {
				targetImg [j].color = new Color (targetImg [j].color.r, targetImg [j].color.g, targetImg [j].color.b, alpha);
			}
			for (int j = 0; j < targetText.Length; j++) {
				targetText [j].color = new Color (targetText [j].color.r, targetText [j].color.g, targetText [j].color.b, alpha);
			}

			yield return null;
		}
		//フェードイン終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}


	public IEnumerator fadeout2 (float ProcessingTime, Image[] targetImg, Text[] targetText, TargetFunc func = null) {
		float alpha = 0;
		float time = 0;

		//フェードアウト
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = 1 - time / ProcessingTime;
			for (int j = 0; j < targetImg.Length; j++) {
				targetImg [j].color = new Color (targetImg [j].color.r, targetImg [j].color.g, targetImg [j].color.b, alpha);
			}
			for (int j = 0; j < targetText.Length; j++) {
				targetText [j].color = new Color (targetText [j].color.r, targetText [j].color.g, targetText [j].color.b, alpha);
			}

			yield return null;
		}
		//フェードアウト終了後の処理
		if (func != null) {
			func ();
		} else {
			Debug.Log ("EndProcess is NULL");
		}
	}


}
