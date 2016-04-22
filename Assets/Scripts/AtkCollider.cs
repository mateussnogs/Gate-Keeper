using UnityEngine;
using System.Collections;

public class AtkCollider : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter2D(Collider2D other) {
		
		if (gameObject.tag == "AtkAmelia") {
			if (other.gameObject.tag == "Knight")
				other.gameObject.GetComponent<Knight> ().GetHit ();
		} else if (gameObject.tag == "AtkKnight") {
			if (other.gameObject.tag == "Amelia") {
				other.gameObject.GetComponent<Amelia> ().GetHit ();
			}
		}
	}
}
