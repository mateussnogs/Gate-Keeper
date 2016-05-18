using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Amelia : MonoBehaviour {
	public bool jumping, movingRight, movingLeft, facingRight, attacking, grounded, attacked, climbingUp, climbingDown, climbing, canClimbUp, canClimbDown, isUp, isDown, isThrowing;
	private AttackMode[] attackOptions = {AttackMode.Axe, AttackMode.Sword, AttackMode.Spear};
	private float swordUpTime = 1;
	private float spearDownTime = 0.66f;
	private float axeDownTime = 0.75f;
	private Dictionary<AttackMode, float> attackTimes;
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
	public AtkCollider atkCollider;
	public GameObject ground2;
	public GameObject penhasco;

	public GameObject spear;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), rightArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), leftArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), upArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), downArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), atkButton.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), jumpButton.GetComponent<Collider2D>());
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();

		atkCollider = transform.GetChild (0).gameObject.GetComponent<AtkCollider>();
		attackTimes = new Dictionary<AttackMode, float> ();
		attackTimes.Add (AttackMode.Axe, axeDownTime);
		attackTimes.Add (AttackMode.Spear, spearDownTime);
		attackTimes.Add (AttackMode.Sword, swordUpTime);
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
		anim.SetBool ("Climbing", false);
		climbing = false;
		GetComponent<Rigidbody2D> ().gravityScale = 2;
	}
		
	public void Attack(AttackMode atkMode, float atkTime) {
		attacking = true;
		anim.SetBool ("Ground", true); //???? sei la que porra eh essa
		atkCollider.atkMode = atkMode;
		if (atkMode == AttackMode.Axe)
			anim.Play ("AxeDown");
		else if (atkMode == AttackMode.Spear)
			anim.Play ("SpearDown");
		else if (atkMode == AttackMode.Sword)
			anim.Play ("SwordUp");
		else if (atkMode == AttackMode.ThrowSpear) {
			ThrowSpear ();
		}
		/*if (attackOptions [indexWeapon] == AttackMode.SwordUp) {
			anim.Play ("SwordUp");
		} else if (attackOptions [indexWeapon] == AttackMode.SpearDown) {
			anim.Play ("SpearDown");
		} else if (attackOptions [indexWeapon] == AttackMode.AxeDown) {
			anim.Play ("AxeDown");
		}*/
		//float atkTime = attackTimes [attackOptions [indexWeapon]]; //pega o tempo do atk pelo dicionário
		StartCoroutine (InstantiateAtkCollider (atkTime/2));
		StartCoroutine (StopAttackRoutine (atkTime)); 
	}

	public void ThrowSpear() {
		anim.Play ("ThrowSpear");
		GameObject s = Instantiate (spear, transform.position, spear.transform.rotation) as GameObject;
		if (facingRight)
			s.GetComponent<ThrowingSpear>().dir = new Vector3 (1, 0, 0);
		else
			s.GetComponent<ThrowingSpear>().dir = new Vector3 (-1, 0, 0);
	}

	IEnumerator InstantiateAtkCollider(float seconds) { //Pra não instanciar direto, senão fica feio
		yield return new WaitForSeconds (seconds);
		atkCollider.gameObject.SetActive (true); // bota o Atk collider pra ficar ativo
	}

	IEnumerator StopAttackRoutine(float waitTime) {
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

	public void GetHit() {
		life--;
		StartCoroutine (PiscaVermelho ());
		attacked = true;
		StartCoroutine (AttackedFalse (0.5f));
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
