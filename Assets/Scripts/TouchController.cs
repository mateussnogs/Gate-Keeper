using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {
	public Amelia amelia;
	string buttonTouched;
	List<string> touchedButtons = new List<string>();

	public Text text;

	AtkButton axeButton, swordButton, spearButton;
	Button leftButton, rightButton, jumpButton;

	// Use this for initialization
	void Start () {
		axeButton = GameObject.FindGameObjectWithTag ("AxeButton").GetComponent<AtkButton> ();
		swordButton = GameObject.FindGameObjectWithTag ("SwordButton").GetComponent<AtkButton>();
		spearButton = GameObject.FindGameObjectWithTag ("SpearButton").GetComponent<AtkButton>();
		leftButton = GameObject.FindGameObjectWithTag ("LeftButton").GetComponent<Button> ();
		rightButton = GameObject.FindGameObjectWithTag ("RightButton").GetComponent<Button> ();
		jumpButton = GameObject.FindGameObjectWithTag ("JumpButton").GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position),
					                   Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Button"));
				buttonTouched = hit.collider.gameObject.name;
				//text.text = buttonTouched;
				if (buttonTouched == "LeftArrow" || buttonTouched == "RightArrow")
					touchedButtons.Add (buttonTouched);
				if (hit.collider != null) {
					if (!amelia.climbing) { // não pode fazer nada se estiver escalando
						if (buttonTouched == "RightArrow") {
							amelia.movingRight = true;
							rightButton.SetTransparent (false);
						} else if (buttonTouched == "JumpButton") {
							amelia.Jump ();					
							jumpButton.SetTransparent (false);
						} else if (buttonTouched == "LeftArrow") {
							amelia.movingLeft = true;
							leftButton.SetTransparent (false);
						} /*else if (buttonTouched == "AttackButton") {
							amelia.Attack ();
						}*/ else if (buttonTouched == "UpArrow") {
							amelia.SwitchWeapon (true);
						} else if (buttonTouched == "DownArrow") {
							amelia.SwitchWeapon (false);
						} else if (buttonTouched == "AxeButton") {
							axeButton.OnTouch ();
							axeButton.SetTransparent (false);
						} else if (buttonTouched == "SwordButton") {
							swordButton.OnTouch ();
							axeButton.SetTransparent (false);
						} else if (buttonTouched == "SpearButton") {
							spearButton.SetTransparent (false);
							spearButton.OnTouch ();
						}
					}
					if (buttonTouched == "ClimbButton") {
						amelia.climbing = true;
						if (amelia.canClimbUp)
							amelia.climbingUp = true;
						else if (amelia.canClimbDown)
							amelia.climbingDown = true;	
					}
				}
			}
		}
		//DisplayTouchList ();
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch (i);
			if (touch.phase == TouchPhase.Ended) {
				switch(touchedButtons[i]) {
					case "LeftArrow":
						touchedButtons.Remove (touchedButtons [i]);
						amelia.movingLeft = false;
						break;
					case "RightArrow":
						touchedButtons.Remove (touchedButtons [i]);
						amelia.movingRight = false;
						break;
				}

			}
		}
	}

	void DisplayTouchList() {
		string r = "";
		foreach (string s in touchedButtons)
			r += " " + s;
		text.text = r;
	}
}
