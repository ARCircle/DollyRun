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


	void Start () {
		startTime = Time.time;
		f = 0;
		anim_move = GetComponent<Animator> ();
		anim_motion = transform.Find ("UTC_Default").gameObject.GetComponent<Animator> ();
		trokko = GameObject.Find ("Trokko");
		fadeOut = GameObject.Find ("FadeOut");
		fadeOut.SetActive (false);
	}

	void Update () {
		t = Time.time - startTime;
		if (t >= 1 && t < 2) {
			if (f == 0) {
				anim_motion.SetTrigger ("Walk");
				f = 1;
			}
		}
		if (t >= 2 && t < 3) {
			if (f == 1) {
				anim_motion.SetTrigger ("Salute");
				f = 2;
			}
		}
		if (t >= 2.5f && t < 3.5f) {
			if (f == 2) {
				anim_move.SetTrigger ("Ride");
				anim_motion.SetTrigger ("Walk");
				f = 3;
			}
		}
		if (t >= 3.7f && t < 4.5f) {
			if (f == 3) {
				anim_motion.SetTrigger ("Jump");
				f = 4;
			}
		}
		if (t > 5.2f) {
			trokko.transform.position += new Vector3 (0, 0, 0.1f);
		}
		if (t > 7) {
			fadeOut.SetActive (true);
		}
	}
}
