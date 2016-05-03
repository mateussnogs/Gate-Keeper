using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	bool active;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive(bool active) {
		this.active = active;
		if (active) {
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<Collider2D> ().enabled = true;
		} else {
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<Collider2D> ().enabled = false; 
		}
			
			
	}
}
