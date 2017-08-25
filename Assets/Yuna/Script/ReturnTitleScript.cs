using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitleScript : MonoBehaviour {

	private GameObject fadeOut;
	private AudioSource audio1;
	private AudioSource audio2;

	void Start () {
		fadeOut = transform.Find ("FadeOut").gameObject;
		fadeOut.SetActive (false);
		AudioSource[] audiosources = GameObject.Find("audios").GetComponents<AudioSource> ();
		audio1 = audiosources [0];
		audio2 = audiosources [1];
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
		
	public void ReturnTitle() {
		SceneManager.LoadScene ("Tittle");
	}

	public void Retry() {
		SceneManager.LoadScene ("GameScene");
	}
}