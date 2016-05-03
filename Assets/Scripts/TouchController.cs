using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {
	public Amelia amelia;
	string buttonTouched;
	List<string> touchedButtons = new List<string>();

	public Text text;

	// Use this for initialization
	void Start () {
	
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
						} else if (buttonTouched == "JumpButton") {
							amelia.Jump ();							
						} else if (buttonTouched == "LeftArrow") {
							amelia.movingLeft = true;
						} else if (buttonTouched == "AttackButton") {
							amelia.Attack ();
						} else if (buttonTouched == "UpArrow") {
							amelia.SwitchWeapon (true);
						} else if (buttonTouched == "DownArrow") {
							amelia.SwitchWeapon (false);
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
