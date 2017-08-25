using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowLineSupport : MonoBehaviour {

	TutorialManager mng;

	// Use this for initialization
	void Start () {
		mng = GameObject.Find ("TutorialCanvas").GetComponent <TutorialManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
