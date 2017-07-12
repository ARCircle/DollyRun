using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("d"))
            transform.position += speed * transform.right * Time.deltaTime;
        if (Input.GetKey("a"))
            transform.position -= speed * transform.right * Time.deltaTime;
    }
}
