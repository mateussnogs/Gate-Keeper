﻿using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {
	public Amelia amelia;
	AtkButton axeButton, swordButton, spearButton, defendButton;
	Button leftArrowButton, rightArrowButton, jumpButton, escadaButton;
	float spearPressedTime, spearTimeAcc;
	bool spearPressed;
	// Use this for initialization
	void Start () {
		axeButton = GameObject.FindGameObjectWithTag ("AxeButton").GetComponent<AtkButton> ();
		swordButton = GameObject.FindGameObjectWithTag ("SwordButton").GetComponent<AtkButton>();
		spearButton = GameObject.FindGameObjectWithTag ("SpearButton").GetComponent<AtkButton>();
		leftArrowButton = GameObject.FindGameObjectWithTag ("LeftButton").GetComponent<Button> ();
		rightArrowButton = GameObject.FindGameObjectWithTag ("RightButton").GetComponent<Button> ();
		//jumpButton = GameObject.FindGameObjectWithTag ("JumpButton").GetComponent<Button> ();
		escadaButton = GameObject.FindGameObjectWithTag ("EscadaButton").GetComponent<Button> ();
		defendButton = GameObject.FindGameObjectWithTag ("DefendButton").GetComponent<AtkButton> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			amelia.SwitchWeapon (true);
		else if(Input.GetKeyDown(KeyCode.DownArrow))
			amelia.SwitchWeapon (false);

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			amelia.Jump ();
			jumpButton.SetTransparent (false);
		} */else if (Input.GetKeyDown (KeyCode.Z))
			axeButton.OnTouch ();
		else if (Input.GetKeyDown (KeyCode.X))
			swordButton.OnTouch ();
		else if (Input.GetKeyDown (KeyCode.C)) {
			spearTimeAcc = Time.time;
			amelia.anim.SetBool ("ThrowingSpear", true);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			defendButton.OnTouch ();
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (!amelia.climbing)
				amelia.movingLeft = true;
			leftArrowButton.SetTransparent (false);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (!amelia.climbing)
				amelia.movingRight = true;
			rightArrowButton.SetTransparent (false);
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow))
			amelia.movingLeft = false;
		if (Input.GetKeyUp (KeyCode.RightArrow))
			amelia.movingRight = false;
		
		if (Input.GetKeyUp (KeyCode.C)) {
			amelia.anim.SetBool ("ThrowingSpear", false);
			if ((Time.time - spearTimeAcc) >= 0.4f)
				spearButton.OnTouch (true);
			else
				spearButton.OnTouch (false);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			escadaButton.SetTransparent (false);
			if (amelia.canClimbUp) {
				
				amelia.climbing = true;
				amelia.climbingUp = true;

			}
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			escadaButton.SetTransparent (false);
			if (amelia.canClimbDown) {				
				amelia.climbing = true;
				amelia.climbingDown = true;
			}
		}
			

	}
}
