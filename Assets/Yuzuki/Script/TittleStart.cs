using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleStart : MonoBehaviour {

	public void StartTitle () {
		transform.parent.Find ("text").gameObject.GetComponent<TouchStart> ().DoNonactive ();
	}
}
