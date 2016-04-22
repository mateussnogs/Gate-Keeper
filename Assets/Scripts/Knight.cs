using UnityEngine;
using System.Collections;

public class Knight : MonoBehaviour {
	Animator anim;
	Vector3 origin;
	Vector3 destiny;
	public float boundLeft, boundRight, speed, atkReach, atkCooldown, atkTimeAcc;
	public bool movingRight, movingLeft, facingRight, attacking, attacked, defBroke, begun;
	public float life;

	public bool backingOff;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool ("Moving", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0)
			Die ();
	}
		
	public void Move(bool right) {
		float distToWalk;
		if (!backingOff) {
			if (right)
				distToWalk = 0.2f;
			else
				distToWalk = -0.2f;

			anim.SetBool ("Moving", true);
			if (right && !facingRight)
				Flip ();
			else if (!right && facingRight)
				Flip ();
			origin = transform.position;
			if (CanWalk (right, origin + new Vector3 (distToWalk, 0, 0)))
				destiny = origin + new Vector3 (distToWalk, 0, 0);
			else
				destiny = origin;
			transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * speed);
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private bool CanWalk(bool right, Vector3 nextPos) {
		if (attacked || attacking || backingOff)
			return false;
		if (right) {
			if (nextPos.x <= boundRight)
				return true;
			else
				return false;
		} else {
			if (nextPos.x >= boundLeft)
				return true;
			else
				return false;
		}
	}

	public bool EnemyInReach(Vector3 enemyPos) {
		Vector3 pos = transform.position;
		if (enemyPos.x > pos.x && enemyPos.x <= pos.x + atkReach ||
			enemyPos.x < pos.x && enemyPos.x >= pos.x - atkReach)
			return true;
		return false;
	}

	public void Attack() {
		if (!attacked && !backingOff) {			
			if (Time.time >= atkTimeAcc) {
				attacking = true;
				transform.GetChild (0).gameObject.SetActive (true);
				atkTimeAcc = Time.time + atkCooldown;
				anim.Play ("Attack");
				StartCoroutine(StopAtk(0.2f));
			}
		}
	}

	public void GetHit() {
		
		if (!ShieldBroken ()) {
			anim.Play ("Stand");
			backingOff = true;
			begun = true;
		} else {
			life--;
			attacked = true;		
			anim.Play ("Attacked");
			StartCoroutine (StopBeingAttacked (0.5f));
		}
	}

	IEnumerator StopBeingAttacked(float attackedTimeAnim) {
		yield return new WaitForSeconds(attackedTimeAnim);
		attacked = false;
	}

	IEnumerator StopAtk(float atkTime) {
		yield return new WaitForSeconds(atkTime);
		attacking = false;
		transform.GetChild (0).gameObject.SetActive (false);
	}

	void Die() {
		gameObject.SetActive (false);
	}

	public bool ShieldBroken() {
		int chanceBreakShield = Random.Range (1, 11);
		if (chanceBreakShield >= 8)
			return true;
		else
			return false;
	}

	public void BackOff() {
		if (begun) {
			anim.SetBool ("Moving", false);
			begun = false;
			origin = transform.position;
			if (facingRight)
				destiny = transform.position + new Vector3 (-1f, 0, 0);
			else
				destiny = transform.position + new Vector3 (1f, 0, 0);
		}
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * speed * 3);
		if (transform.position.x == destiny.x)
			backingOff = false;
	}
}
