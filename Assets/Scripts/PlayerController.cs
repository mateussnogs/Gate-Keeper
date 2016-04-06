using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float maxSpeed = 10f;
	public float jumpForce = 300f;
	bool facingRight = false;
	public float ameliaVelocity = 5f;
	bool grounded = false;
	float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public MouseController mc;

	public Amelia amelia;

	Animator anim;


	void Start () {
		amelia = GetComponent<Amelia> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);

		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	//Inverte o componente x da escala do player
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update() {		
		ProcessKeyboard ();
		ProcessState ();

	}

	void ProcessKeyboard() {
		if (Input.GetKeyDown (KeyCode.Z)) {
			anim.Play ("SwordUp");
		} else if (Input.GetKeyDown (KeyCode.X)) {
			anim.Play ("SwordDown");
		} else if (Input.GetKeyDown (KeyCode.C)) {
			anim.Play ("SpearDown");
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
			
	}
		

	void Jump() {
		if (grounded) {
			anim.SetBool ("Ground", false);
			amelia.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		}
	}

	void ProcessState() {
		if (amelia.movingRight) {
			amelia.MoveRight ();
		} else if (amelia.movingLeft)
			amelia.MoveLeft ();
		/*else if (amelia.jumping) {
			amelia.Jump ();
		} else if (amelia.attacking) {
			amelia.Attack ();
		} */else {
			amelia.anim.SetBool ("Moving", false);
		}
	}
}
