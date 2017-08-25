using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	private float t;
	private float startTime;
	private int f;

	private GameObject gameoverText;
	private GameObject panel;
	private GameObject returnTitleCanvas;

	private ParticleSystem ps;
	private AudioSource[] audios;


	void Start () {
		startTime = Time.time;
		f = 0;

		audios = GameObject.Find ("sounds").GetComponents<AudioSource> ();

		gameoverText = GameObject.Find ("GameOverText");
		gameoverText.SetActive (false);
		panel = GameObject.Find ("Panel");
		panel.SetActive (false);
		returnTitleCanvas = GameObject.Find ("ReturnTitleCanvas");
		returnTitleCanvas.SetActive (false);
	}


	void Update () {
		t = Time.time - startTime;

		if (t > 1) {
			if (f == 0) {
				audios [1].Play();
				f = 1;
			}
		}

		if (t > 1.5f) {
			gameoverText.SetActive (true);
		}

		if (t > 3) {
			panel.SetActive (true);
			panel.transform.Find ("scoreText").GetComponent<Text>().text = GrobalClass.ScoreCalc ().ToString ();
		}

		if (t > 4.9f) {
			if (f == 1) {
				audios [2].Play();
				f = 2;
			}
		}

		if (t > 4.5f) {
			//ハイスコアだったら
			if (ScoreCalculator.LatestScoreNum != -1) {
				panel.GetComponent<Animator> ().SetTrigger ("HighScore");
			}
		}
	}


	public void SetActiveCanvas() {
		if (t > 6.5f) {
			returnTitleCanvas.SetActive (true);
			GameObject.Find ("screenButton").GetComponent<AudioSource> ().Play ();
		}
	}
}