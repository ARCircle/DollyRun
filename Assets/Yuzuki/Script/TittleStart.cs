using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleStart : MonoBehaviour {

	public AudioSource audioSorce;


	public void StartTitle () {
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().TouchStartSE.Play ();
		transform.parent.Find ("text").gameObject.GetComponent<TouchStart> ().DoNonactive ();
		GameObject.Find ("UTC_Default").GetComponent <Animator>().SetTrigger ("Jump");
		GameObject.Find ("ps_start").GetComponent <ParticleSystem>().Play();
	}
}
