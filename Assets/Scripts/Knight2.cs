using UnityEngine;
using System.Collections;

public class Knight2 : Enemy {
	public GameObject gate;
	public bool canHitTower = false;
	public float stunnedTime;
	float stunnedTimeAcc;
	GameObject atkReflect;

	void Start() {
		gate = GameObject.FindGameObjectWithTag ("Gate");
		atkReflect = transform.GetChild (1).gameObject;
		base.Start ();
		id = ID.Knight;
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

	void Die() {
		Destroy (gameObject);
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

	void AtkReflect() {
		atkReflect.GetComponent<Animator> ().Play ("AtkReflect");
	}

	
}
