using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Amelia : MonoBehaviour {
	public bool jumping, movingRight, movingLeft, facingRight, attacking, grounded, attacked, climbingUp, climbingDown, climbing, canClimbUp, canClimbDown, isUp, isDown, isThrowing, isShielded;
	private AttackMode[] attackOptions = {AttackMode.Axe, AttackMode.Sword, AttackMode.Spear};
	private float swordUpTime = 1;
	private float spearDownTime = 0.66f;
	private float axeDownTime = 0.75f;
	public float defendTime = 0.5f;
	public int numWeapons = 3;
	private int indexWeapon = 2;
	public float speed = 1f;
	public Animator anim;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround, KnightMask;
	Vector3 origin;
	Vector3 destiny;
	float journeyLength;
	public float boundLeft, boundRight;
	public float atkReach;
	public int life, numPiscadas;
	public float climbSpeed = 1;
	SpriteRenderer sprite;
	public GameObject weaponChoosenIcon;


	public float jumpForce = 300f;

	public GameObject rightArrowCollider;
	public GameObject leftArrowCollider;
	public GameObject upArrowCollider;
	public GameObject downArrowCollider;
	public GameObject atkButton;
	public GameObject jumpButton;
	public AtkColliderAmelia atkCollider;
	public GameObject ground2;
	public GameObject penhasco;

	public Spear spear;
	public Sword sword;
	public Axe axe;
	public GameObject spearGO;
	public GameObject shield;
	GameObject protection;
	AudioSource getHitSound;

	// Use this for initialization
	void Start () {
		getHitSound = GetComponent<AudioSource> ();
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Button"));
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();

		atkCollider = transform.GetChild (0).gameObject.GetComponent<AtkColliderAmelia>();
		shield = transform.GetChild (1).gameObject;
		protection = transform.GetChild (2).gameObject;

		spear = new Spear (this, 1, 0.66f, spearGO);
		sword = new Sword (this, 1, 1);
		axe = new Axe (this, 2, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (life <= 0)
			Die ();
		if (transform.position.y > 2.8)
			sprite.sortingOrder = -4;
		else
			sprite.sortingOrder = 0;
		
	}

	void FixedUpdate() {
		if (!climbing) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			if (!attacking)
				anim.SetBool ("Ground", grounded);
		}
	}

	public void Move(bool right) {
		if ((!attacking || !grounded)) { 
			float distToWalk;
			if (right)
				distToWalk = 0.2f;
			else
				distToWalk = -0.2f;
			
			anim.SetBool ("Moving", true);
			if (right && !facingRight)
				ChangeDirection ();
			else if (!right && facingRight)
				ChangeDirection ();
			origin = transform.position;
			if (CanWalk (right, origin + new Vector3 (distToWalk, 0, 0)))
				destiny = origin + new Vector3 (distToWalk, 0, 0);
			else
				destiny = origin;
			if (!isThrowing) // pode até mudar de direção, mas não se move enquanto estiver preparando para lançar
				transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * speed);
		}
	}

	private bool CanWalk(bool right, Vector3 nextPos) {
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

	public void Jump() {
		if (grounded) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}

	public void Climb(bool up) {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), ground2.GetComponent<Collider2D>());
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"));
		anim.SetBool ("Climbing", true);
		GetComponent<Rigidbody2D> ().gravityScale = 0;
		Vector3 destiny;

		if (up) {
			destiny = transform.position + new Vector3 (0, 1, 0);
		} else {
			destiny = transform.position + new Vector3 (0, -1, 0);
		}
		transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * climbSpeed);
	}

	public void StopClimbing() {
		if (climbingUp)
			climbingUp = false;
		else
			climbingDown = false;
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), ground2.GetComponent<Collider2D>(), false);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Enemy"), false);
		anim.SetBool ("Climbing", false);
		climbing = false;
		GetComponent<Rigidbody2D> ().gravityScale = 2;
	}
		
	public void Attack(AttackMode atkMode) {
		attacking = true;
		anim.SetBool ("Ground", true); //???? sei la que porra eh essa
		atkCollider.atkMode = atkMode;
		if (atkMode == AttackMode.Axe)
			axe.Attack ();
		else if (atkMode == AttackMode.Spear)
			spear.Attack ();
		else if (atkMode == AttackMode.Sword)
			sword.Attack ();
		else if (atkMode == AttackMode.ThrowSpear) {
			spear.Throw ();
		}
	}

	public void Defend() {
		anim.Play ("Defense");
		protection.GetComponent<Animator> ().Play ("AmeliaProtection");
		shield.SetActive (true);
		StartCoroutine(DeactivateShield (defendTime));
		isShielded = true;
	}

	public IEnumerator DeactivateShield(float time) {
		yield return new WaitForSeconds (time);
		isShielded = false;
		shield.SetActive (false);
	}

	public void ThrowSpear() {
		
	}

	public IEnumerator InstantiateAtkCollider(float seconds) { //Pra não instanciar direto, senão fica feio
		yield return new WaitForSeconds (seconds);
		atkCollider.gameObject.SetActive (true); // bota o Atk collider pra ficar ativo
	}

	public IEnumerator StopAttackRoutine(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		attacking = false;
		atkCollider.gameObject.SetActive (false);
	}

	IEnumerator AttackedFalse(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		attacked = false;
	}
	public void SwitchWeapon(bool trocaPraDireita) {
		
		if (trocaPraDireita) {			
			if (indexWeapon + 1 <= numWeapons - 1) {
				weaponChoosenIcon.transform.position += new Vector3 (1, 0, 0);
				indexWeapon++;
			}
		} else {			
			if (indexWeapon - 1 >= 0) {
				weaponChoosenIcon.transform.position -= new Vector3 (1, 0, 0);
				indexWeapon--;
			}
		}
	}

	public void GetHit(GameObject enemy) {
		//if (!isShielded || (isShielded && ((facingRight && enemy.transform.position.x < transform.position.x)// caso em que tem shield
		//	|| (!facingRight && enemy.transform.position.x > transform.position.x)))) {		  //  mas não na direção do atk
		if (!isShielded) {
			getHitSound.Play ();
			life--;
			StartCoroutine (PiscaVermelho ());
			attacked = true;
			StartCoroutine (AttackedFalse (0.5f));
		}
	}

	public void IgnoraColisaoPenhasco() {
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), penhasco.GetComponent<Collider2D>());
	}

	IEnumerator PiscaVermelho() {
		
		if (GetComponent<SpriteRenderer> ().color == Color.red)
			GetComponent<SpriteRenderer> ().color = Color.white;
		else
			GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.2f);
		if (++numPiscadas <= 4)
			StartCoroutine (PiscaVermelho ());
		else {
			GetComponent<SpriteRenderer> ().color = Color.white;
			numPiscadas = 0;
		}
	}

	void ChangeDirection() { //aka Flip()
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}		

	void Die() {
		gameObject.SetActive (false);
		SceneManager.LoadScene ("GameOver");
	}

	public Vector3 ViewPortPosition() {
		return Camera.main.WorldToViewportPoint (transform.position);
	}

		
}
