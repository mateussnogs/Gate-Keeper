using UnityEngine;
using System.Collections;

public class ThrowingSpear : MonoBehaviour {
	[HideInInspector]
	public Vector3 dir;
	public float speed;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = dir * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Knight" || other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<Enemy> ().Attacked (1);
			Destroy (gameObject);
		}
	}
}
