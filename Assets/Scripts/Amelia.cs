using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Amelia : MonoBehaviour {
	public bool jumping, movingRight, movingLeft, facingRight, attacking, grounded;
	private AttackMode[] attackOptions = {AttackMode.AxeDown, AttackMode.SwordUp, AttackMode.SpearDown};
	public int numWeapons = 3;
	private int indexWeapon = 2;
	public float speed = 0.3f;
	public Animator anim;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	Vector3 origin;
	Vector3 destiny;
	public bool moveBegun;
	float journeyLength;
	public float boundLeft;

	public GameObject weaponChoosenIcon;

	public float jumpForce = 300f;

	public GameObject rightArrowCollider;
	public GameObject leftArrowCollider;
	public GameObject upArrowCollider;
	public GameObject downArrowCollider;
	public GameObject atkButton;
	public GameObject jumpButton;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), rightArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), leftArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), upArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), downArrowCollider.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), atkButton.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (GetComponent<Collider2D> (), jumpButton.GetComponent<Collider2D>());
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
	}
		
	public void MoveRight() {
		anim.SetBool ("Moving", true);
		if (!facingRight)
			Flip ();
		origin = transform.position;
		destiny = origin + new Vector3 (speed, 0, 0);
		journeyLength = Vector3.Distance (origin, destiny);
		moveBegun = false;
		transform.position = Vector3.Lerp (origin, destiny, journeyLength);	
	}

	public void MoveLeft() {
		anim.SetBool ("Moving", true);
		if (facingRight)
			Flip ();
		origin = transform.position;
		if ((origin - new Vector3 (speed, 0, 0)).x >= boundLeft)			
			destiny = origin - new Vector3 (speed, 0, 0);
		journeyLength = Vector3.Distance (origin, destiny);
		moveBegun = false;

		transform.position = Vector3.Lerp (origin, destiny, journeyLength);	
	}

	public void Jump() {
		if (grounded) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}
		
	public void Attack() {
		movingLeft = movingRight = false;
		if (attackOptions [indexWeapon] == AttackMode.SwordUp) {
			anim.Play ("SwordUp");
		} else if (attackOptions [indexWeapon] == AttackMode.SpearDown) {
			anim.Play ("SpearDown");
		} else if (attackOptions [indexWeapon] == AttackMode.AxeDown) {
			anim.Play ("AxeDown");
		}

	}

	public void SwitchWeapon(bool trocaPraDireita) {
		
		if (trocaPraDireita) {			
			if (indexWeapon + 1 <= numWeapons - 1) {
				weaponChoosenIcon.transform.position += new Vector3 (1, 0, 0);
				indexWeapon++;
				print (indexWeapon);
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



		
}
