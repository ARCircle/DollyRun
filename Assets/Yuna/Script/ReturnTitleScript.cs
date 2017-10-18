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

	public void TweetButton() {
		string scoretext = GrobalClass.ScoreCalc ().ToString ();
		string text1 = "新感覚トロッコランゲーム「DollyRun」でスコア";
		string text2 = "点でた！\n";
		string text3 = "探検";
		string text4 = "m コイン";
		string text5 = "枚\n";
		string url = "http://arcircle.net/\n";
		string hashtag = "#AR会 #DollyRun";
		string message = text1 + scoretext + text2 + text3 + GrobalClass.distance + text4 + GrobalClass.coins + text5 + url + hashtag;
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(message));
	}
		
	public void ReturnTitle() {
		SceneManager.LoadScene ("Tittle");
	}

	public void Retry() {
		SceneManager.LoadScene ("GameScene");
	}
}