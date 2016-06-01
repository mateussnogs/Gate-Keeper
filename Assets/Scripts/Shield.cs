using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	Amelia amelia;
	public int hp = 2;
	bool broken = false;
	// Use this for initialization
	void Start () {
		amelia = transform.parent.gameObject.GetComponent<Amelia> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hp <= 0)
			broken = true;
	}

	void OnTriggerEnter2D(Collider2D other) {		
		if (other.gameObject.layer == LayerMask.NameToLayer ("EnemyAtk")) {
			hp--;
		}	
	}
}
