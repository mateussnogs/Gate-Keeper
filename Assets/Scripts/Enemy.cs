using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public enum State {Walking, Attacking, Attacked, Waiting, Defending, RunningAway};
	public enum AmeliaLocation {Right, Left, Found};
	[HideInInspector]
	public enum ID {Wyvern, Knight, Unknown};
	public int life;
	public State state = State.Waiting;
	public ID id = ID.Unknown;
	public float distanceToStop;
	[HideInInspector]
	public Animator anim;
	public float waitTime, walkTime, walkSpeed, attackTime = 1, attackedTime, defendTime, runAwayTime;
	[HideInInspector]
	public float waitTimeAcc, walkTimeAcc, attackTimeAcc, attackedTimeAcc, defendTimeAcc, runAwayTimeAcc;
	public bool stateBegun = false; // state begun begins only at the scope of the state, but not on the switch
	public bool facingRight = false;
	[HideInInspector]
	public Amelia amelia;
	[HideInInspector]
	public GameObject atkCollider;

	// Use this for initialization
	public virtual void Start () {
		anim = GetComponent<Animator> ();
		amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia>();
		if (transform.childCount > 0 && transform.GetChild(0) != null)
			atkCollider = transform.GetChild (0).gameObject;
	}

	// Update is called once per frame
	public virtual void Update () {		
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
		case State.RunningAway:
			RunAway ();
			break;
		}
	}

	public virtual void Walk() {
		if (!stateBegun) {
			stateBegun = true;
			walkTimeAcc = Time.time + walkTime;
		}
		if (Time.time > walkTimeAcc) {
			anim.SetBool ("Walk", false);
			SwitchState (State.Waiting, "Stand");
		} else {
			bool changeState = Move (); // true quando achou a amelia, aí é pra atacar
			if (changeState) {
				anim.SetBool ("Walk", false);
				SwitchState (State.Attacking, "Attack");
			}

		}

	}

	public virtual void Attack() {
		if (!stateBegun) {
			stateBegun = true;
			attackTimeAcc = Time.time + attackTime;
			StartCoroutine(InstantiateAtk(attackTime*0.85f));
		}
		if (Time.time > attackTimeAcc) {
			anim.SetBool ("Attack", false);
			if (atkCollider != null)
				atkCollider.SetActive (false);
			SwitchState (State.Waiting, "Stand");
		}
	}

	public virtual IEnumerator InstantiateAtk(float timeToWait) {
		yield return new WaitForSeconds (timeToWait);
		if (atkCollider != null)
			atkCollider.SetActive (true);
	}
	public virtual void GetHit() {
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

	public virtual void Attacked() { // não tem a ver com o estado Attacked diretamente
		CleanAnimationStateMachine (); // State setado quando atacado e com prioridade maior. Por isso limpa a machine state.
		if (Defended () && id == ID.Knight)
			SwitchState (State.Defending, "Defend");
		else if (id == ID.Knight)
			SwitchState (State.Attacked, "Attacked"); // vai chamar GetHit por estar no estado Attacked
		else if (id == ID.Wyvern)
			SwitchState (State.RunningAway, "RunAway");
	}

	Vector3 destiny;
	public virtual void Defend() {		
		if (!stateBegun) {
			stateBegun = true;
			defendTimeAcc = Time.time + defendTime;

			if (facingRight && !IsCloseToLeftBound ())
				destiny = transform.position + new Vector3 (-1, 0, 0);
			else if (!facingRight && !IsCloseToRightBound ())
				destiny = transform.position + new Vector3 (1, 0, 0);
			else
				destiny = transform.position;
		}
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime*walkSpeed*3);
		if (Time.time > defendTimeAcc) {
			anim.SetBool ("Defend", false);
			SwitchState (State.Waiting, "Stand");
		}
	}

	public virtual bool Defended() {
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

	public virtual void CleanAnimationStateMachine() {
		anim.SetBool ("Attack", false);
		anim.SetBool ("Stand", false);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Attacked", false);
		anim.SetBool ("Defend", false);
	}

	public virtual void Wait() {
		if (!stateBegun) {
			stateBegun = true;
			waitTimeAcc = Time.time + waitTime;
		}
		if (Time.time > waitTimeAcc) {
			anim.SetBool ("Stand", false);
			SwitchState (State.Walking, "Walk");
		}
	}

	public virtual void RunAway() {
		if (!stateBegun) {
			stateBegun = true;
			runAwayTimeAcc = Time.time + runAwayTime;
		}
		if (Time.time > runAwayTimeAcc) {
			SwitchState (State.Waiting, "Stand");
		} else {
			RunFromTo (amelia.gameObject, transform.position);
		}
	}

	public virtual void RunFromTo(GameObject go, Vector3 dest) {
		if (facingRight) {
			if (IsCloseToRightBound ()) {
				ChangeDirection ();
				destiny = transform.position + new Vector3 (-1, 0, 0);
			} else
				destiny = transform.position + new Vector3 (1, 0, 0);
		}
		else if (!facingRight) {			
			if (IsCloseToLeftBound ()) {
				ChangeDirection ();
				destiny = transform.position + new Vector3 (1, 0, 0);
			}
			else
				destiny = transform.position + new Vector3 (-1, 0, 0);
		}
		else 
			destiny = transform.position;
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * walkSpeed);
	}

	public virtual void SwitchState(State newState, string animVar) {
		if (atkCollider != null)
			atkCollider.SetActive (false); // gambiarra pra sempre garantir que o atkcollider seja desligado
		state = newState;
		stateBegun = false;
		ChangeAnimationState (animVar, true);
	}

	public virtual void ChangeAnimationState(string varCond, bool newValue) {
		bool curValue = anim.GetBool (varCond);
		if (curValue != newValue)
			curValue = newValue;
		anim.SetBool (varCond, curValue);
	}

	public virtual bool Move() { // retorna true se for pra mudar de estado(quando acha a vagabunda)		
		Vector3 origin = transform.position;
		Vector3 destiny = origin;
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

	public virtual AmeliaLocation FindAmelia() {
		if (amelia.transform.position.x > transform.position.x + distanceToStop)
			return AmeliaLocation.Right;
		else if (amelia.transform.position.x < transform.position.x - distanceToStop)
			return AmeliaLocation.Left;
		else
			return AmeliaLocation.Found;
	}

	public virtual void ChangeDirection() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public Vector3 ViewPortPosiition() {
		return Camera.main.WorldToViewportPoint (transform.position);
	}

	public bool IsCloseToLeftBound() {
		if (ViewPortPosiition ().x >= 0 && ViewPortPosiition().x <= 0.1) {
			return true;
		}
		return false;
	}
	public bool IsCloseToRightBound() {
		if (ViewPortPosiition ().x >= 0.9 && ViewPortPosiition().x <= 1) {
			return true;
		}
		return false;
	}


}
