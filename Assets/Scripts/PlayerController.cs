using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Amelia amelia;

	Animator anim;


	void Start () {
		amelia = GetComponent<Amelia> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {		
	}		

	void Update() {		
		ProcessState ();

	}		
		

	void ProcessState() {
		if (amelia.movingRight) {
			amelia.Move(true);
		} else if (amelia.movingLeft)
			amelia.Move(false);
		else {
			amelia.anim.SetBool ("Moving", false);
		}
	}
}
