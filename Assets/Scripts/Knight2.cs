using UnityEngine;
using System.Collections;

public class Knight2 : Enemy {


	void Start() {
		base.Start ();
		id = ID.Knight;
	}

	void Update() {
		base.Update ();
		if (life <= 0)
			Die ();
	}

	void OnTriggerEnter2D(Collider2D other) {
	}

	void Die() {
		Destroy (gameObject);
	}
	
}
