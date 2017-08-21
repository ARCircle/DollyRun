﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public GameObject pausePanel;
	bool pausing = false;
	bool fading = false;
	float gameovertime = 2f;
	Animator[] anims;
	Fader fade = new Fader ();

	// Use this for initialization
	void Start () {
		anims = transform.root.GetComponentsInChildren<Animator> ();
		StartCoroutine (fade.blackout (1f, DeletePanel));
	}
	
	// Update is called once per frame
	void Update () {
		if (GrobalClass.StartInterval > 0f) {
			GrobalClass.StartInterval -= Time.deltaTime;
		}
		if (GrobalClass.gameover) {
			gameovertime -= Time.deltaTime;
			if (gameovertime < 0f) {
				SceneManager.LoadScene ("GameOverScene");				
			} else if (gameovertime < 1f && !fading) {
				ScoreCalculator.UpdateTopScore((int)(GrobalClass.distance + GrobalClass.coins) * 10);
				StartCoroutine (fade.blackin (1f, DeletePanel));
				fading = true;
			}
		}
	}

	public void Push(){
		GrobalClass.pause = !GrobalClass.pause;
		pausing = !pausing;
		pausePanel.SetActive (pausing);
		foreach (Animator an in anims) {
			an.enabled = !pausing;
		} 
		/*if (pausing) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}*/
	}

	void DeletePanel () {
		Destroy (GameObject.Find ("BlackPlate(Clone)"));
	}
}
