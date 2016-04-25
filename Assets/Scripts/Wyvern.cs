using UnityEngine;
using System.Collections;

public class Wyvern : MonoBehaviour {
	public Amelia amelia;
	public Fireball fireball;
	public float shootCooldown;
	float shootTimeAcc = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		Attack ();
	}

	void Attack() {
		if (Time.time > shootTimeAcc) {
			Shoot (amelia.gameObject);
			shootTimeAcc = Time.time + shootCooldown;
		}
	}

	void Shoot(GameObject go) {		
		Fireball fb = Instantiate (fireball, transform.position, Quaternion.identity) as Fireball;
		fb.target = go;
		fb.targetDir = (go.transform.position - transform.position).normalized;
		/*float speed = fb.speed;
		fb.GetComponent<Rigidbody2D> ().velocity = go.transform.position - transform.position * speed;*/

	}
}
