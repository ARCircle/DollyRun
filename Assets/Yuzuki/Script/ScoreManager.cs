using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	Fader fade = new Fader ();
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void StartScoreMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();
		GetComponent <Canvas> ().enabled = true;

		StartCoroutine (fade.fadein (1.0f, imgs));
	}

	public void EndScoreMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();

		StartCoroutine (fade.fadeout (1.0f, imgs, DisenableScoreCanvas));
	}

	void DisenableScoreCanvas () {
		GetComponent <Canvas> ().enabled = false;
	}
}
