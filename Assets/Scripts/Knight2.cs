using UnityEngine;
using System.Collections;

public class Knight2 : Enemy {
	public GameObject gate;
	public bool canHitTower = false;
	void Start() {
		gate = GameObject.FindGameObjectWithTag ("Gate");
		base.Start ();
		id = ID.Knight;
	}

	void Update() {
		base.Update ();
		if (IsAmeliaHigher ()) {
			canHitTower = true;
			target = gate;
		} else {
			canHitTower = false;
			target = amelia.gameObject;
		}
		if (life <= 0)
			Die ();
	}		

	void Die() {
		Destroy (gameObject);
	}

	bool IsAmeliaHigher() {
		if (amelia.transform.position.y - transform.position.y >= 4)
			return true;
		return false;
	}


	
}
