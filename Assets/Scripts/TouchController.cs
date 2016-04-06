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
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position), Vector2.zero);
				buttonTouched = hit.collider.gameObject.name;
				text.text = buttonTouched;
				touchedButtons.Add (buttonTouched);
				if (hit.collider != null) {
					if (buttonTouched == "RightArrow") {
						amelia.movingRight = true;
						amelia.moveBegun = true;
					} else if (buttonTouched == "JumpButton") {
						//amelia.jumping = true;
						amelia.Jump();
					} else if (buttonTouched == "LeftArrow") {
						amelia.movingLeft = true;
					} else if (buttonTouched == "AttackButton") {
						//amelia.attacking = true;
						amelia.Attack();
					} else if (buttonTouched == "UpArrow") {
						amelia.SwitchWeapon (true);
					} else if (buttonTouched == "DownArrow") {
						amelia.SwitchWeapon (false);
					}
				}
			}
		}
		for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch (i);
			if (touch.phase == TouchPhase.Ended) {
				switch(touchedButtons[i]) {
					case "LeftArrow":
						amelia.movingLeft = false;
						break;
					case "RightArrow":
						amelia.movingRight = false;
						break;
					case "JumpButton":
						amelia.jumping = false;
						break;
				}
				touchedButtons.Remove (touchedButtons [i]);
			}
		}
	}
}
