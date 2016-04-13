using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {
	public Amelia amelia;
	string buttonTouched;
	public bool clickMoving;
	public Text text;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) {
			//RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition),
				                   Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Button"));
			if (hit != null && hit.collider != null) {
				buttonTouched = hit.collider.gameObject.name;
				text.text = buttonTouched;
				if (hit.collider != null) {
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