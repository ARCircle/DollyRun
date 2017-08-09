using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchOtherButton : MonoBehaviour {

	Fader fade = new Fader ();
	//スタートボタンを押したときの処理
	public void TouchStart () {
		//ボタン入力を禁止する


		//カメラの親子関係をリセット
		GameObject.Find("Camera").transform.SetParent(null);
		//アニメション再生
		Animation playerMove = GameObject.Find("PlayerSystem").GetComponent <Animation> ();
		playerMove.Play ();

		StartCoroutine (StartGame());
	}

	IEnumerator StartGame () {
		yield return new WaitForSeconds (1.0f);
		Debug.Log ("fade開始");
		//yield return fade.blackout ();
	}
}
