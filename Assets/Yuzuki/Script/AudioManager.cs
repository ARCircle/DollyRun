using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	AudioSource[] au;
	public AudioSource TouchStartSE;
	public AudioSource DecisionSE;
	public AudioSource backSE;


	// Use this for initialization
	void Start () {
		au = GetComponents <AudioSource> ();
		TouchStartSE = au [0];
		DecisionSE = au [1];
		backSE = au [2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
