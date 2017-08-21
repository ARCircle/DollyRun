using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

	private float t;
	private float startTime;
	private int f;

	private GameObject fadeOut;
	private GameObject gameoverText;

	private ParticleSystem ps;
	private AudioSource[] audios;


	void Start () {
		startTime = Time.time;
		f = 0;

		audios = GameObject.Find ("audios").GetComponents<AudioSource> ();

		gameoverText = GameObject.Find ("GameOverText");
		gameoverText.SetActive (false);
		fadeOut = GameObject.Find ("FadeOut");
		fadeOut.SetActive (false);
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
			fadeOut.SetActive (true);
		}

		if (t > 4) {
			//シーン遷移
			SceneManager.LoadScene("OpeningScene");
		}
	}
}
