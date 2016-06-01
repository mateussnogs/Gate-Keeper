using UnityEngine;
using System.Collections;

public class Spear : Weapon {	
	GameObject spear;
	public static int weaponBreakChance = 0;

	public Spear(Amelia amelia, int dmg, float atkTime, GameObject spear) : base(amelia, dmg, atkTime) {
		this.spear = spear;
	}

	public override void Attack() {
		amelia.anim.Play ("SpearDown");
		amelia.StartCoroutine (amelia.InstantiateAtkCollider (atkTime/2));
		amelia.StartCoroutine (amelia.StopAttackRoutine (atkTime)); 
	}

	public void Throw() {
		amelia.anim.Play ("ThrowSpear");
		GameObject s = GameObject.Instantiate (spear, amelia.transform.position, spear.transform.rotation) as GameObject;
		if (amelia.facingRight)
			s.GetComponent<ThrowingSpear>().dir = new Vector3 (1, 0, 0);
		else
			s.GetComponent<ThrowingSpear>().dir = new Vector3 (-1, 0, 0);
		amelia.StartCoroutine(amelia.StopAttackRoutine (0.2f));
	}
}
