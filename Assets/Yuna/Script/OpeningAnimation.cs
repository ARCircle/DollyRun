using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAnimation : MonoBehaviour {

	private float t;
	private float startTime;
	private int f;

	private Animator anim_move;
	private Animator anim_motion;
	private GameObject trokko;
	private GameObject fadeOut;

	private AudioSource[] audios;


	void Start () {
		startTime = Time.time;
		f = 0;

		audios = GameObject.Find("audios").GetComponents<AudioSource>();

		anim_move = GetComponent<Animator> ();
		anim_motion = transform.Find ("UTC_Default").gameObject.GetComponent<Animator> ();
		trokko = GameObject.Find ("Trokko");
		fadeOut = GameObject.Find ("FadeOut");
		fadeOut.SetActive (false);
	}


	void Update () {
		t = Time.time - startTime;

		if (t >= 0.5f && t < 1) {
			if (f == 0) {
				audios[0].Play ();  //ぴょい
				f = 1;
			}
		}

		else if (t >= 1 && t < 1.5f) {
			if (f == 1) {
				anim_motion.SetTrigger ("Walk");
				audios[1].Play ();  //ぴょこぴょこ
				f = 2;
			}
		}

		else if (t >= 2 && t < 2.5f) {
			if (f == 2) {
				anim_motion.SetTrigger ("Salute");
				audios[1].Stop ();  //ぴょこぴょこ終了
				audios[2].Play ();  //いっくよー！
				f = 3;
			}
		}

		else if (t >= 2.5f && t < 3.7f) {
			if (f == 3) {
				anim_move.SetTrigger ("Ride");
				anim_motion.SetTrigger ("Walk");
				audios[1].PlayDelayed (0.5f);  //ぴょこぴょこ
				f = 4;
			}
		}

		else if (t >= 3.7f && t < 5.2f) {
			if (f == 4) {
				anim_motion.SetTrigger ("Jump");
				audios[1].Stop ();  //ぴょこぴょこ終了
				audios[3].PlayDelayed (0.25f);  //びょい～ん
				audios[4].PlayDelayed (0.8f);  //発進！
				f = 5;
			}
		}

		else if (t > 5.2f) {
			trokko.transform.position += new Vector3 (0, 0, 0.1f);
			if (f == 5) {
				audios[5].Play ();  //ガタンゴトン
				f = 6;
			}
			audios[5].volume -= 0.02f;

			if (t > 7) {
				fadeOut.SetActive (true);
			}

			if (t > 8) {
				//シーン遷移
			}
		}
	}
}
