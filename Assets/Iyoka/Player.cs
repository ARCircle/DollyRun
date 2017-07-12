using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public PlayerItemControl item;
	public PlayerMoveControl move;

	// Use this for initialization
	void Start () {
		item = GetComponent<PlayerItemControl> ();
		move = GetComponent<PlayerMoveControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
