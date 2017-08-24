using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStopScript : MonoBehaviour {

	private GameObject fadeOut;
	private AudioSource audio1;
	private AudioSource audio2;
	private AudioSource audio3;

	void Start () {
		fadeOut = GameObject.Find ("FadeOut");
		fadeOut.SetActive (false);
		AudioSource[] audiosources = GameObject.Find("audios").GetComponents<AudioSource> ();
		audio1 = audiosources [0];
		audio2 = audiosources [1];
		audio3 = audiosources [2];
		gameObject.SetActive (false);
	}

	void Update () {
		
	}


	public void ReturnTitleButton() {
		fadeOut.SetActive (true);
		transform.Find ("ReturnTitleButton").GetComponent<Animation> ().Play ();
		audio1.Play ();
		Invoke("ReturnTitle", 1);
	}

	public void RetryButton() {
		fadeOut.SetActive (true);
		transform.Find ("RetryButton").GetComponent<Animation> ().Play ();
		audio2.Play ();
		Invoke("Retry", 1);
	}

	public void ReturnGameButton() {
		//一時停止終わり
		audio3.Play ();
	}

	public void ReturnTitle() {
		SceneManager.LoadScene ("Tittle");
	}

	public void Retry() {
		SceneManager.LoadScene ("GameScene");
	}
}
