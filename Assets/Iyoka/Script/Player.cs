using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public PlayerItemControl item;
	public PlayerMoveControl move;
	Transform body;

	// Use this for initialization
	void Start () {
		item = GetComponent<PlayerItemControl> ();
		move = GetComponent<PlayerMoveControl> ();
		body = gameObject.transform.Find ("Trokko");
	}

	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis ("Horizontal");
		body.Translate (x * Time.deltaTime * 4f, 0f, 0f);
	}
}
