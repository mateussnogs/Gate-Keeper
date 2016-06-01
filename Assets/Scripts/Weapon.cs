using UnityEngine;
using System.Collections;

public class Weapon {
	public float atkTime;
	public int dmg;
	public Amelia amelia;
	// Use this for initialization
	public Weapon(Amelia amelia, int dmg, float atkTime) {
		this.dmg = dmg;
		this.atkTime = atkTime;
		this.amelia = amelia;
	}
	public virtual void Attack() {
		
	}
}
