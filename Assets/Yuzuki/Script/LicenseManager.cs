using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LicenseManager : MonoBehaviour {

	Fader fade;
	// Use this for initialization
	void Awake () {
		fade = this.gameObject.AddComponent <Fader> ();
	}

	public void StartMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();
		Text[] texts = GetComponentsInChildren <Text> ();
		GetComponent <Canvas> ().enabled = true;
		StartCoroutine (fade.fadein2 (0.2f, imgs, texts));
	}

	public void EndMenu () {
		Image[] imgs = GetComponentsInChildren <Image> ();
		Text[] texts = GetComponentsInChildren <Text> ();
		StartCoroutine (fade.fadeout2 (0.2f, imgs, texts, DisenableScoreCanvas));
	}

	void DisenableScoreCanvas () {
		GetComponent <Canvas> ().enabled = false;
	}
}

