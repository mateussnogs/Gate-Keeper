﻿using UnityEngine;
using System.Collections;

public class Wyvern : Enemy {
	// Use this for initialization
	public Fireball fireball;
	void Start () {
		base.Start ();
		id = ID.Wyvern;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if (state == State.RunningAway)
			GetComponent<Collider2D> ().enabled = false;
		else
			GetComponent<Collider2D> ().enabled = true;
	}

	public override bool Move ()
	{
		AmeliaLocation ameliaLocation = FindAmelia ();
		if (facingRight && ameliaLocation == AmeliaLocation.Left) {
			ChangeDirection ();
		} else if (!facingRight && ameliaLocation == AmeliaLocation.Right)
			ChangeDirection ();
		return true;
	}

	public override void Attack() {
		if (!stateBegun) {
			stateBegun = true;
			attackTimeAcc = Time.time + attackTime;
			Fireball fb = Instantiate (fireball, transform.position, fireball.transform.rotation) as Fireball;
			fb.targetDir = (amelia.transform.position - transform.position).normalized;
		}
		if (Time.time > attackTimeAcc) {
			anim.SetBool ("Attack", false);
			if (atkCollider != null)
				atkCollider.SetActive (false);
			SwitchState (State.Waiting, "Stand");
		}
	}

}
