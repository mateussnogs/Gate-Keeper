﻿using UnityEngine;
using System.Collections;

public class ColliderEscalar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia") {
			other.GetComponent<Amelia> ().StopClimbing ();
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia") {
			other.GetComponent<Amelia> ().canClimb = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia") {
			other.GetComponent<Amelia> ().canClimb = false;
		}
	}
}
