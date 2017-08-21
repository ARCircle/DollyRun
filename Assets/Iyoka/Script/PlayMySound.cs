using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMySound : MonoBehaviour {
	AudioSource audios;

	// Use this for initialization
	void Start () {
	}

	public void PlaySound() {
		audios = GetComponent<AudioSource> ();
		audios.PlayOneShot (audios.clip);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
