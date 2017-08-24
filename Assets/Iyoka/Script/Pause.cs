using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public GameObject pausePanel;
    public GameObject pauseButton;
	bool isgameover = false;
	bool pausing = false;
	bool fading = false;
	float gameovertime = 2f;
	//int gccount = 1;
	Animator[] anims;
	Fader fade;// = new Fader ();
	AudioSource auds;

	// Use this for initialization
	void Start () {
		GrobalClass.Reset ();
		fade = gameObject.AddComponent<Fader> ();
		anims = transform.root.GetComponentsInChildren<Animator> ();
		StartCoroutine (fade.blackout (1f, DeletePanel));
		auds = GameObject.Find ("audios").GetComponents<AudioSource> ()[2];
	}
	
	// Update is called once per frame
	void Update () {
		if (GrobalClass.StartInterval > 0f) {
			GrobalClass.StartInterval -= Time.deltaTime;
		}
		if (GrobalClass.gameover) {
			if (!isgameover) {
				isgameover = true;
				ScoreCalculator.UpdateTopScore ((int)(GrobalClass.distance + GrobalClass.coins) * 10);
			}
			gameovertime -= Time.deltaTime;
			if (gameovertime < 0f) {
				SceneManager.LoadScene ("GameOverScene");				
			} else if (gameovertime < 1f && !fading) {
				fading = true;
				StartCoroutine (fade.blackin (1f, DeletePanel));
			}
		} else {
			ScoreCalculator.UpdateTmpScore ((int)(GrobalClass.distance + GrobalClass.coins) * 10);
		}
		/*if (GrobalClass.distance - 500f * gccount > 0f) {
			Resources.UnloadUnusedAssets();
			System.GC.Collect ();
			gccount++;
		}*/
	}

	public void Push(){
		if (GrobalClass.StartInterval <= 0f && !GrobalClass.gameover) {
			GrobalClass.pause = !GrobalClass.pause;
			pausing = !pausing;
			if (pausing)
				auds.Play ();
			gameObject.SetActive (!pausing);
			pausePanel.SetActive (pausing);
			pauseButton.SetActive (pausing);
			foreach (Animator an in anims) {
				an.enabled = !pausing;
			} 
			/*if (pausing) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}*/
		}
	}

	void DeletePanel () {
		Destroy (GameObject.Find ("BlackPlate(Clone)"));
	}
}
