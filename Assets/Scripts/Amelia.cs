using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Amelia : MonoBehaviour {
	public bool jumping, movingRight, movingLeft, facingRight, attacking, grounded;
	private AttackMode[] attackOptions = {AttackMode.AxeDown, AttackMode.SwordUp, AttackMode.SpearDown};
	private float swordUpTime = 0.6f;
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

	public GameObject weaponChoosenIcon;

	public float jumpForce = 300f;

	public GameObject rightArrowCollider;
	public GameObject leftArrowCollider;
	public GameObject upArrowCollider;
	public GameObject downArrowCollider;
	public GameObject atkButton;
	public GameObject jumpButton;
	public GameObject atkCollider;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), rightArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), leftArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), upArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), downArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), atkButton.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), jumpButton.GetComponent<Collider2D>());
		anim = GetComponent<Animator> ();

		attackTimes = new Dictionary<AttackMode, float> ();
		attackTimes.Add (AttackMode.AxeDown, axeDownTime);
		attackTimes.Add (AttackMode.SpearDown, spearDownTime);
		attackTimes.Add (AttackMode.SwordUp, swordUpTime);
	}
	
	// Update is called once per frame
	void Update () {		
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (!attacking)
			anim.SetBool ("Ground", grounded);
	}

	public void Move(bool right) {
		if (!attacking || !grounded) { 
			float distToWalk;
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
	public void MoveRight() {
		if (!attacking || grounded == false) {
			anim.SetBool ("Moving", true);
			if (!facingRight)
				Flip ();
			origin = transform.position;
			if ((origin + new Vector3 (0.2f, 0, 0)).x <= boundRight)
				destiny = origin + new Vector3 (0.2f, 0, 0);
			else
				destiny = origin;
			journeyLength = Vector3.Distance (origin, destiny);
			//GetComponent<Rigidbody2D> ().MovePosition(transform.position + new Vector3(speed*0.4f, 0, 0));
			//transform.position = Vector3.Lerp (origin, destiny, journeyLength);
			transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * speed);
		}
	}

	public void MoveLeft() {
		if (!attacking || grounded == false) {
			anim.SetBool ("Moving", true);
			if (facingRight)
				Flip ();
			origin = transform.position;
			if ((origin - new Vector3 (0.2f, 0, 0)).x >= boundLeft)
				destiny = origin - new Vector3 (0.2f, 0, 0);
			else
				destiny = origin;
			journeyLength = Vector3.Distance (origin, destiny);

			transform.position = Vector3.MoveTowards (transform.position, destiny, Time.deltaTime * speed);
		}
	}

	public void Jump() {
		if (grounded) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}
		
	public void Attack() {
		transform.GetChild (1).gameObject.SetActive (true); // bota o Atk collider pra ficar ativo
		attacking = true;
		anim.SetBool ("Ground", true);
		if (attackOptions [indexWeapon] == AttackMode.SwordUp) {
			anim.Play ("SwordUp");
		} else if (attackOptions [indexWeapon] == AttackMode.SpearDown) {
			anim.Play ("SpearDown");

		} else if (attackOptions [indexWeapon] == AttackMode.AxeDown) {
			anim.Play ("AxeDown");
		}
		//attackAnimTime = anim.GetCurrentAnimatorStateInfo (0).length;
		StartCoroutine (BackMoving (attackTimes[attackOptions[indexWeapon]])); //pega o tempo do atk pelo dicionário

	}

	IEnumerator BackMoving(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		attacking = false;
		transform.GetChild (1).gameObject.SetActive (false);
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



	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator ShowCurrentClipLength() {
		yield return new WaitForEndOfFrame ();
		print (anim.GetCurrentAnimatorStateInfo (0).length);
	}


		
}
