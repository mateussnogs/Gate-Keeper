using UnityEngine;
using System.Collections;

public class AtkColliderKnight : MonoBehaviour {
	public Gate gate;
	Knight2 knight;
	Amelia amelia;
	// Use this for initialization
	void Start () {
		gate = GameObject.FindGameObjectWithTag ("Gate").GetComponent<Gate> ();
		knight = transform.parent.gameObject.GetComponent<Knight2> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Amelia") {
			amelia = other.gameObject.GetComponent<Amelia> ();
			if (knight.state != Knight2.State.Attacked)
				amelia.GetHit (knight.gameObject);
		} else if (other.gameObject.tag == "Gate") {
			if (transform.parent.gameObject.GetComponent<Knight2> ().canHitTower)
				gate.GetHit (1);
		} else if (other.gameObject.tag == "ShieldBlock") {
			// Deflect();
		}
	}
}
