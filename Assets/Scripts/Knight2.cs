using UnityEngine;
using System.Collections;

public class Knight2 : MonoBehaviour {
	public enum State {Walking, Attacking, Attacked, Waiting, Defending};
	enum AmeliaLocation {Right, Left, Found};
	public int life;
	public State state = State.Waiting;
	public float distanceToStop;
	Animator anim;
	public float waitTime, walkTime, walkSpeed, attackTime = 1, attackedTime, defendTime;
	float waitTimeAcc, walkTimeAcc, attackTimeAcc, attackedTimeAcc, defendTimeAcc;
	bool stateBegun = false; // state begun begins only at the scope of the state, but not on the switch
	bool facingRight = false;
	Amelia amelia;
	GameObject atkCollider;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia>();
		atkCollider = transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case State.Attacked:
			GetHit();
			break;
		case State.Defending:
			Defend ();
			break;
		case State.Attacking:
			Attack ();
			break;
		case State.Waiting:
			Wait ();
			break;		
		case State.Walking:
			Walk ();
			break;
		}
	}

	void Walk() {
		if (!stateBegun) {
			stateBegun = true;
			walkTimeAcc = Time.time + walkTime;
		}
		if (Time.time > walkTimeAcc) {
			anim.SetBool ("Walk", false);
			SwitchState (State.Waiting, "Stand");
		} else {
			bool changeState = Move ();
			if (changeState) {
				anim.SetBool ("Walk", false);
				SwitchState (State.Attacking, "Attack");
			}

		}

	}

	void Attack() {
		if (!stateBegun) {
			stateBegun = true;
			attackTimeAcc = Time.time + attackTime;
			StartCoroutine(InstantiateAtk(attackTime*0.85f));
		}
		if (Time.time > attackTimeAcc) {
			anim.SetBool ("Attack", false);
			atkCollider.SetActive (false);
			SwitchState (State.Waiting, "Stand");
		}
	}

	IEnumerator InstantiateAtk(float timeToWait) {
		yield return new WaitForSeconds (timeToWait);
		atkCollider.SetActive (true);
	}
	void GetHit() {
		if (!stateBegun) {
			stateBegun = true;
			attackedTimeAcc = Time.time + attackedTime;
		}
		if (Time.time > attackedTimeAcc) {
			life--;
			anim.SetBool ("Attacked", false);
			SwitchState (State.Waiting, "Stand");
		}

	} // ou Attacked()

	public void Attacked() {
		CleanAnimationStateMachine ();
		if (Defended ())
			SwitchState (State.Defending, "Defend");
		else			
			SwitchState (State.Attacked, "Attacked");
	}

	Vector3 destiny;
	void Defend() {		
		if (!stateBegun) {
			stateBegun = true;
			defendTimeAcc = Time.time + defendTime;

			if (facingRight)
				destiny = transform.position + new Vector3 (-1, 0, 0);
			else
				destiny = transform.position + new Vector3 (1, 0, 0);
		}
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime*walkSpeed*3);
		if (Time.time > defendTimeAcc) {
			anim.SetBool ("Defend", false);
			SwitchState (State.Waiting, "Stand");
		}
	}

	bool Defended() {
		int roll = Random.Range (1, 11);
		if (state == State.Attacking || (amelia.facingRight && facingRight) //Se estiver atacando, ou olhando para lado contrário
			|| (!amelia.facingRight && !facingRight)) { 						// Não defende certamente!
				return false;
		}
		else if (roll >= 5) { // 60% de defender
			return true;
		} else
			return false;
	}

	void CleanAnimationStateMachine() {
		anim.SetBool ("Attack", false);
		anim.SetBool ("Stand", false);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Attacked", false);
		anim.SetBool ("Defend", false);
	}

	void Wait() {
		if (!stateBegun) {
			stateBegun = true;
			waitTimeAcc = Time.time + waitTime;
		}
		if (Time.time > waitTimeAcc) {
			anim.SetBool ("Stand", false);
			SwitchState (State.Walking, "Walk");
		}
	}

	void SwitchState(State newState, string animVar) {
		atkCollider.SetActive (false); // gambiarra pra sempre garantir que o atkcollider seja desligado
		state = newState;
		stateBegun = false;
		ChangeAnimationState (animVar, true);
	}

	void ChangeAnimationState(string varCond, bool newValue) {
		bool curValue = anim.GetBool (varCond);
		if (curValue != newValue)
			curValue = newValue;
		anim.SetBool (varCond, curValue);
	}

	bool Move() { // retorna true se for pra mudar de estado(quando acha a vagabunda)
		Vector3 destiny;
		Vector3 origin = transform.position;
		AmeliaLocation ameliaLocation = FindAmelia ();
		if (facingRight && ameliaLocation == AmeliaLocation.Left) {
			ChangeDirection ();
		} else if (!facingRight && ameliaLocation == AmeliaLocation.Right)
			ChangeDirection ();
		else if (ameliaLocation == AmeliaLocation.Found)
			return true;
		if (facingRight)
			destiny = origin + new Vector3 (1, 0, 0);
		else
			destiny = origin + new Vector3 (-1, 0, 0);
		
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * walkSpeed);
		return false;
	}

	AmeliaLocation FindAmelia() {
		if (amelia.transform.position.x > transform.position.x + distanceToStop)
			return AmeliaLocation.Right;
		else if (amelia.transform.position.x < transform.position.x - distanceToStop)
			return AmeliaLocation.Left;
		else
			return AmeliaLocation.Found;
	}

	void ChangeDirection() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	
}
