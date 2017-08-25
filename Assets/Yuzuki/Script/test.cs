using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.GetComponent <TutorialManager> ().NextTutorial ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			this.GetComponent <TutorialManager> ().endActionSupport = true;
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			GrobalClass.pause = !GrobalClass.pause;
		}
	}
}
