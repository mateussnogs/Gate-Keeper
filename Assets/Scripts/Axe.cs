using UnityEngine;
using System.Collections;

public class Axe : Weapon {
	public static int weaponBreakChance = 100; // sempre acerta
	public Axe(Amelia amelia, int dmg, float atkTime) : base(amelia, dmg, atkTime) {
		
	}

	public override void Attack() {
		amelia.anim.Play ("AxeDown");
		amelia.StartCoroutine (amelia.InstantiateAtkCollider (atkTime/2));
		amelia.StartCoroutine (amelia.StopAttackRoutine (atkTime)); 
	}

}
