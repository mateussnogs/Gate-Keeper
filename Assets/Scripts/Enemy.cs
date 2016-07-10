using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public enum State {Walking, Attacking, Attacked, Waiting, Defending, RunningAway, Stunned};
	public enum TargetLocation {Right, Left, Found}; // em geral vai ser amelia!! Mas pode ser o portão também...
	[HideInInspector]
	public enum ID {Wyvern, Knight, KnightSemEscudo, Unknown};
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
	private IEnumerator coroutineAtk;
	public int dmg;
	Text dmgText;
	public GameObject target;
	SpriteRenderer spriteRenderer;
	Color originalColor;
	// Use this for initialization
	public virtual void Start () {
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Button"));
		dmgText = GameObject.FindGameObjectWithTag ("DmgText").GetComponent<Text>();
		anim = GetComponent<Animator> ();
		amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia>();
		if(amelia == null)
			amelia = GameObject.FindGameObjectWithTag ("Amelia").GetComponent<Amelia>();
		else 
			target = amelia.gameObject;
		
		if (transform.childCount > 0 && transform.GetChild(0) != null)
			atkCollider = transform.GetChild (0).gameObject;

		spriteRenderer = GetComponent<SpriteRenderer> ();
		originalColor = spriteRenderer.color;
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
		case State.Stunned:
			Stunned ();
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
			bool changeState = Move (target); // true quando achou a amelia, aí é pra atacar
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
			coroutineAtk = InstantiateAtk (attackTime * 0.7f);
			StartCoroutine(coroutineAtk);
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
		/*if (!stateBegun) {
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
		}*/

	} // ou Attacked()


	public IEnumerator PiscaPreto(float time) {
		spriteRenderer.color = Color.black;
		yield return new WaitForSeconds (time);
		spriteRenderer.color = originalColor;
	}
	public virtual void Attacked(int dmg = 1, int weaponBreakChance = 0) { // não tem a ver com o estado Attacked diretamente
		CleanAnimationStateMachine (); // State setado quando atacado e com prioridade maior. Por isso limpa a machine state.
		this.dmg = dmg;
		if (Defended (weaponBreakChance) && id == ID.Knight)
			SwitchState (State.Defending, "Defend");
		else if (id == ID.Knight || id == ID.KnightSemEscudo) {
			if (coroutineAtk != null)
				StopCoroutine (coroutineAtk);
			SwitchState (State.Attacked, "Attacked"); // vai chamar GetHit por estar no estado Attacked
		} else if (id == ID.Wyvern) {			
			SwitchState (State.Attacked, "Attacked");
		}
	}

	public Vector3 destiny;
	public virtual void Defend() {}

	public virtual bool Defended(int weaponBreakChance) {
		return false;
	}
	/*public virtual bool Defended(int weaponBreakChance) {
		int roll = Random.Range (1, 11);
		if (state == State.Attacking || (amelia.facingRight && facingRight) //Se estiver atacando, ou olhando para lado contrário
			|| (!amelia.facingRight && !facingRight)) { 						// Não defende certamente!
			return false;
		}
		else if (roll >= 5 + weaponBreakChance) { // 60% de defender a princípio
			return true;
		} else
			return false;
	}*/

	public virtual void CleanAnimationStateMachine() {
		anim.SetBool ("Attack", false);
		anim.SetBool ("Stand", false);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Attacked", false);
		anim.SetBool ("Defend", false);
		anim.SetBool ("Stunned", false);
		if (id != ID.Knight && id != ID.KnightSemEscudo)
			anim.SetBool ("RunAway", false);
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

	public virtual void Stunned() {
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

	public virtual bool Move(GameObject target) { // retorna true se for pra mudar de estado(quando acha a vagabunda)		
		Vector3 origin = transform.position;
		Vector3 destiny = origin;
		TargetLocation ameliaLocation = FindTarget (target);
		if (ViewPortPosiition ().x == 0)
			destiny = transform.position;
		else {
			if (facingRight && ameliaLocation == TargetLocation.Left) {
				ChangeDirection ();
			} else if (!facingRight && ameliaLocation == TargetLocation.Right)
				ChangeDirection ();
			else if (ameliaLocation == TargetLocation.Found)
				return true;
			if (facingRight)
				destiny = origin + new Vector3 (1, 0, 0);
			else
				destiny = origin + new Vector3 (-1, 0, 0);
		}

		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * walkSpeed);
		return false;
	}

	public virtual TargetLocation FindTarget(GameObject target) {
		if (target.transform.position.x > transform.position.x + distanceToStop)
			return TargetLocation.Right;
		else if (target.transform.position.x < transform.position.x - distanceToStop)
			return TargetLocation.Left;
		else
			return TargetLocation.Found;
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

	public virtual void Die() {
		Score.IncreaseScore (10);
		Destroy (gameObject);
	}


}
