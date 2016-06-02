using UnityEngine;
using System.Collections;

public class Sword : Weapon {
	public static int weaponBreakChance = 0;

	public Sword(Amelia amelia, int dmg, float atkTime) : base(amelia, dmg, atkTime) {
		
	}

	public override void Attack() {
		amelia.anim.Play ("SwordUp");
		float timeFirstAtk = 0.1f;
		float timeStopFirstAtk = timeFirstAtk + 0.2f;
		float timeSecondAtk = 0.6f;
		float timeStopSecondAtk = atkTime;

		amelia.StartCoroutine (InstantiateAtkCollider (timeFirstAtk, -5));
		amelia.StartCoroutine (amelia.StopAttackRoutine (timeStopFirstAtk)); 

		amelia.StartCoroutine (InstantiateAtkCollider (timeSecondAtk, 6)); // segubdo atk sempre acerta
		amelia.StartCoroutine (amelia.StopAttackRoutine (timeStopSecondAtk)); 
	}


	public IEnumerator InstantiateAtkCollider(float atkTime, int weaponBreakChance) {
		yield return new WaitForSeconds (atkTime);
		Sword.weaponBreakChance = weaponBreakChance;
		amelia.atkCollider.gameObject.SetActive (true); // bota o Atk collider pra ficar ativo
	}
}
