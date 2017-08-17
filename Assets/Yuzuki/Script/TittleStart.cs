using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleStart : MonoBehaviour {

	public AudioSource audioSorce;


	public void StartTitle () {
		GameObject.Find ("AudioManager").GetComponent <AudioManager> ().TouchStartSE.Play ();
		transform.parent.Find ("text").gameObject.GetComponent<TouchStart> ().DoNonactive ();

	}
}
