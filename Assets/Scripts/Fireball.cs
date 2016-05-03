﻿using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public Amelia amelia;
	public float speed = 10;
	public GameObject target;
	Vector3 origin;
	Vector3 destiny;
	public Vector3 targetDir;
	// Use this for initialization
	void Start () {
		amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia>();

		targetDir = (amelia.transform.position - transform.position).normalized;
		GetComponent<Rigidbody2D> ().velocity = targetDir*speed;
	}
	
	// Update is called once per frame//
	void Update () {
	}

	void FollowTarget() {
		targetDir = amelia.transform.position;
		transform.position = Vector3.MoveTowards (transform.position, targetDir, Time.deltaTime * speed);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia") {
			amelia.GetHit ();
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "Ground")
			Destroy (gameObject);
	}
}
