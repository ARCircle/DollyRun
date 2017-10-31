using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchOtherButton : MonoBehaviour {

	Fader fade;
	//スタートボタンを押したときの処理
	public void TouchStart () {
		fade = this.gameObject.AddComponent<Fader> ();


		//カメラの親子関係をリセット
		GameObject.Find("Camera").transform.SetParent(null);
		//アニメション再生
		Animation playerMove = GameObject.Find("PlayerSystem").GetComponent <Animation> ();
		playerMove.Play ();
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().DecisionSE.Play ();

		StartCoroutine (StartGame());
	}

	IEnumerator StartGame () {
		yield return new WaitForSeconds (1.0f);

		yield return fade.blackin (1.5f, ToGameScene);
	}

	void ToGameScene () {
		//GrobalClass.Reset ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("GameScene");
	}



	public void ScoreStart () {
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().DecisionSE.Play ();

		GameObject.Find ("ScoreCanvas").GetComponent <ScoreManager> ().StartScoreMenu ();
	}

	public void LicenseStart () {
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().DecisionSE.Play ();

		GameObject.Find ("LicenseCanvas").GetComponent <LicenseManager> ().StartMenu ();
	}

	public void TutorialStart () {
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().DecisionSE.Play ();

		fade = this.gameObject.AddComponent<Fader> ();
		StartCoroutine (fade.blackin (1.5f, ToTutorial));
	}

	void ToTutorial () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Tutorial");
	}

	public void ScoreEnd () {
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().backSE.Play ();
		GameObject.Find ("ScoreCanvas").GetComponent <ScoreManager> ().EndScoreMenu ();
	}

	public void LicenseEnd () {
		//音声の再生
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().backSE.Play ();
		GameObject.Find ("LicenseCanvas").GetComponent <LicenseManager> ().EndMenu ();
	}
}
