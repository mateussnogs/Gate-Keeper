using UnityEngine;
using System.Collections;

public class Knight2 : Enemy {


	void Start() {
		base.Start ();
		id = ID.Knight;
	}

	void Update() {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia")
			print ("iha");
	}

	
}
