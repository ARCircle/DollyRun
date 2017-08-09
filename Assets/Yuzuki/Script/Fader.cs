using System;
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

	public delegate void TargetFunc ();

	public IEnumerator fadein (float ProcessingTime, Image[] targetImg, TargetFunc func) {
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
		func ();
	}


	public IEnumerator fadeout (float ProcessingTime, Image[] targetImg, TargetFunc func) {
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
		func ();
	}


	public IEnumerator blackin (float ProcessingTime, TargetFunc func) {
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
		func ();
	}


	public IEnumerator blackout (float ProcessingTime, TargetFunc func) {
		float alpha = 0;
		float time = 0;
		Image img = Instantiate (Resources.Load ("BlackPlate") as GameObject).transform.Find ("Image").gameObject.GetComponent <Image> ();

		//フェードイン
		while (time < ProcessingTime) {
			time += Time.deltaTime;
			alpha = 1 - time / ProcessingTime;
			img.color = new Color (0, 0, 0, alpha);

			yield return null;
		}
		//フェードイン終了後の処理
		func ();
	}




}
