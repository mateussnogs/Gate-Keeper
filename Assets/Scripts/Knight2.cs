using UnityEngine;
using System.Collections;

public class Knight2 : Enemy {
	public GameObject gate;
	public bool canHitTower = false;
	public float stunnedTime;
	float stunnedTimeAcc;
	GameObject atkReflect;
	AudioSource defenseSound, getHitSound;

	void Start() {
		AudioSource[] sounds = GetComponents<AudioSource> ();
		defenseSound = sounds [0];
		getHitSound = sounds [1];
		gate = GameObject.FindGameObjectWithTag ("Gate");
		atkReflect = transform.GetChild (1).gameObject;
		base.Start ();
	}

	void Update() {
		base.Update ();
		if (IsAmeliaHigher ()) {
			canHitTower = true;
			target = gate;
		} else {
			canHitTower = false;
			target = amelia.gameObject;
		}
		if (life <= 0)
			Die ();
	}		
		
	public override void Defend() {		
		if (!stateBegun) {
			defenseSound.Play ();
			stateBegun = true;
			defendTimeAcc = Time.time + defendTime;

			if (ViewPortPosiition ().x == 0)
				destiny = transform.position; // nao se mexe se estiver perto do limite
			else {
				if (facingRight && !IsCloseToLeftBound ())
					destiny = transform.position + new Vector3 (-1, 0, 0);
				else if (!facingRight && !IsCloseToRightBound ())
					destiny = transform.position + new Vector3 (1, 0, 0);
				else
					destiny = transform.position;
			}
		}
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime*walkSpeed*3);
		if (Time.time > defendTimeAcc) {
			anim.SetBool ("Defend", false);
			SwitchState (State.Waiting, "Stand");
		}
	}



	bool IsAmeliaHigher() {
		if (amelia.transform.position.y - transform.position.y >= 4)
			return true;
		return false;
	}

	public void Stun() {
		AtkReflect ();
		CleanAnimationStateMachine (); // State setado quando atacado e com prioridade maior. Por isso limpa a machine state.
		SwitchState (State.Stunned, "Stunned");
	}
	public override void Stunned() {		
		if (!stateBegun) {			
			stateBegun = true;
			stunnedTimeAcc = Time.time + stunnedTime;
		}
		if (Time.time > stunnedTimeAcc) {
			anim.SetBool ("Stunned", false);
			SwitchState (State.Waiting, "Stand");
		}

	} // ou Attacked()


	public override void GetHit() {
		if (!stateBegun) {
			getHitSound.Play ();
			StartCoroutine (PiscaPreto (0.2f));
			stateBegun = true;
			attackedTimeAcc = Time.time + attackedTime;
		}
		if (Time.time > attackedTimeAcc) {
			
			life -= dmg;
			anim.SetBool ("Attacked", false);
			if (id == ID.Wyvern)
				SwitchState (State.RunningAway, "RunAway");
			else
				SwitchState (State.Waiting, "Stand");
		}

	} // ou Attacked()

	void AtkReflect() {
		atkReflect.GetComponent<Animator> ().Play ("AtkReflect");
	}

	public override bool Defended (int weaponBreakChance)
	{
		if (id == ID.KnightSemEscudo) {
			return false;
		}
		int roll = Random.Range (1, 11);
		if (state == State.Attacking || (amelia.facingRight && facingRight) //Se estiver atacando, ou olhando para lado contrário
			|| (!amelia.facingRight && !facingRight)) { 						// Não defende certamente!
			return false;
		}
		else if (roll >= 5 + weaponBreakChance) { // 60% de defender a princípio
			return true;
		} else
			return false;
	}



	
}
