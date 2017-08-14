using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
	public GameObject pausePanel;
	bool pausing = false;
	Animator[] anims;

	// Use this for initialization
	void Start () {
		anims = transform.root.GetComponentsInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Push(){
		GrobalClass.pause = !GrobalClass.pause;
		pausing = !pausing;
		pausePanel.SetActive (pausing);
		foreach (Animator an in anims) {
			an.enabled = !pausing;
		} 
		if (pausing) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}
}
