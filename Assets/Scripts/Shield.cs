using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	Amelia amelia;
	// Use this for initialization
	void Start () {
		amelia = transform.parent.gameObject.GetComponent<Amelia> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {		
		if (other.gameObject.layer == LayerMask.NameToLayer ("EnemyAtk")) {
		}	
	}
}
