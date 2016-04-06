using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {
	public Amelia amelia;
	string buttonTouched;
	public bool clickMoving;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			buttonTouched = hit.collider.gameObject.name;
			if (hit.collider != null) {
				if (buttonTouched == "RightArrow") {
					amelia.movingRight = true;
					amelia.moveBegun = true;
				} else if (buttonTouched == "JumpButton") {
					amelia.jumping = true;
				} else if (buttonTouched == "LeftArrow") {
					amelia.movingLeft = true;
				} else if (buttonTouched == "AttackButton") {
					amelia.attacking = true;
				} else if (buttonTouched == "UpArrow") {
					amelia.SwitchWeapon (true);
				} else if (buttonTouched == "DownArrow") {
					amelia.SwitchWeapon (false);
				}
			}
		}
		else if(Input.GetMouseButtonUp(0)) {
			//RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			//buttonTouched = hit.collider.gameObject.name;
			//if (hit.collider != null) {
				if (buttonTouched == "RightArrow") {
					amelia.movingRight = false;
				} else if (buttonTouched == "JumpButton") {
					amelia.jumping = false;
				} else if (buttonTouched == "LeftArrow") {
					amelia.movingLeft = false;
				} else if (buttonTouched == "AttackButton") {
					amelia.attacking = false;
				}
			//}
		}
	}
}