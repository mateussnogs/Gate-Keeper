using UnityEngine;
using System.Collections;

public class AtkColliderAmelia : MonoBehaviour {
	public AttackMode atkMode;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other) {			
			if (other.gameObject.tag == "Knight" || other.gameObject.tag == "Enemy") {
				switch (atkMode) {
				case AttackMode.Spear:
					other.gameObject.GetComponent<Enemy> ().Attacked (1);
					break;
				case AttackMode.Axe:					
					other.gameObject.GetComponent<Enemy> ().Attacked (2);
					break;
				case AttackMode.Sword:
					other.gameObject.GetComponent<Enemy> ().Attacked (1);
					break;
				}
			}
	}
}
