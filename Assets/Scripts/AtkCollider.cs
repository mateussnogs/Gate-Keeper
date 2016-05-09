using UnityEngine;
using System.Collections;

public class AtkCollider : MonoBehaviour {
	// Use this for initialization
	public AttackMode atkMode;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnTriggerEnter2D(Collider2D other) {
		
		if (gameObject.tag == "AtkAmelia") {
			if (other.gameObject.tag == "Knight" || other.gameObject.tag == "Enemy") {
				switch (atkMode) {
				case AttackMode.SpearDown:
					other.gameObject.GetComponent<Enemy> ().Attacked (1);
					break;
				case AttackMode.AxeDown:
					other.gameObject.GetComponent<Enemy> ().Attacked (2);
					break;
				case AttackMode.SwordUp:
					other.gameObject.GetComponent<Enemy> ().Attacked (1);
					break;
				}
			}

				//other.gameObject.GetComponent<Knight2> ().Attacked ();
		} else if (gameObject.tag == "AtkKnight") {
			if (other.gameObject.tag == "Amelia") {
				Amelia amelia = other.gameObject.GetComponent<Amelia> ();
				Knight2 knight = transform.parent.gameObject.GetComponent<Knight2> ();
				if (knight.state != Knight2.State.Attacked)
					amelia.GetHit ();
			}
		}
	}
}
