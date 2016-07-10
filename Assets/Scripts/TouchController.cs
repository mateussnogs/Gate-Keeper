using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {
	public Amelia amelia;
	string buttonTouched;
	List<string> touchedButtons = new List<string>();
	Dictionary<int, string> touchesIds = new Dictionary<int, string> ();

	public Text text1;
	public Text text2;

	AtkButton axeButton, swordButton, spearButton, defendButton;
	Button leftButton, rightButton, jumpButton, escadaButton;

	float spearTimeAcc;

	public GameObject analogico;

	// Use this for initialization
	void Start () {
		axeButton = GameObject.FindGameObjectWithTag ("AxeButton").GetComponent<AtkButton> ();
		swordButton = GameObject.FindGameObjectWithTag ("SwordButton").GetComponent<AtkButton>();
		spearButton = GameObject.FindGameObjectWithTag ("SpearButton").GetComponent<AtkButton>();
		leftButton = GameObject.FindGameObjectWithTag ("LeftButton").GetComponent<Button> ();
		rightButton = GameObject.FindGameObjectWithTag ("RightButton").GetComponent<Button> ();
		//jumpButton = GameObject.FindGameObjectWithTag ("JumpButton").GetComponent<Button> ();
		defendButton = GameObject.FindGameObjectWithTag ("DefendButton").GetComponent<AtkButton> ();
		escadaButton = GameObject.FindGameObjectWithTag ("EscadaButton").GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (touch.position),
					                   Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Button"));
				buttonTouched = hit.collider.gameObject.name;
				//text.text = buttonTouched;
				if (buttonTouched == "LeftArrow" || buttonTouched == "RightArrow") {
					//touchedButtons.Add (buttonTouched);
					touchesIds [touch.fingerId] = buttonTouched;
				}
				if (hit.collider != null) {
					if (!amelia.climbing) { // não pode fazer nada se estiver escalando
						if (buttonTouched == "RightArrow") {
							amelia.movingRight = true;
							rightButton.SetTransparent (false);
						} else if (buttonTouched == "JumpButton") {
							amelia.Jump ();					
							jumpButton.SetTransparent (false);
						} else if (buttonTouched == "DefendButton") {
							defendButton.OnTouch ();
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
						} else if (buttonTouched == "SwordButton") {
							swordButton.OnTouch ();
						} else if (buttonTouched == "SpearButton") {
							if (spearButton.CanAct ()) {
								amelia.isThrowing = true;
								touchesIds [touch.fingerId] = buttonTouched;
								amelia.movingLeft = amelia.movingRight = false;
								amelia.anim.SetBool ("ThrowingSpear", true);
								spearTimeAcc = Time.time;
							}
						}
					}
					if (buttonTouched == "ClimbButton") {
						escadaButton.SetTransparent (false);

						if (amelia.canClimbUp) {
							amelia.climbing = true;
							amelia.climbingUp = true;
						} else if (amelia.canClimbDown) {
							amelia.climbing = true;
							amelia.climbingDown = true;	
						}
					}
				}
			}
		}
		//DisplayTouchList ();

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Ended) {
				string buttonToRemove = touchesIds[touch.fingerId];
				switch(buttonToRemove) {
					case "LeftArrow":
						touchesIds.Remove (touch.fingerId);
						amelia.movingLeft = false;
						break;
					case "RightArrow":
						touchesIds.Remove (touch.fingerId);
						amelia.movingRight = false;
						break;
				case "SpearButton":
					touchesIds.Remove (touch.fingerId);
					amelia.isThrowing = false;
						if ((Time.time - spearTimeAcc) >= 0.4f) {
							spearButton.OnTouch (true);
							amelia.anim.Play ("ThrowSpear");
						} else {
							spearButton.OnTouch (false);
						}				
						amelia.anim.SetBool ("ThrowingSpear", false);
						break;
				}
			}
		}

		/*for (int i = 0; i < Input.touchCount; i++) {
			Touch touch = Input.GetTouch (i);
			//text2.text = touch.fingerId.ToString();
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
					case "SpearButton":
						if ((Time.time - spearTimeAcc) >= 0.4f) {
							spearButton.OnTouch (true);
							amelia.anim.Play ("ThrowSpear");
						} else {
							spearButton.OnTouch (false);
						}
						touchedButtons.Remove (touchedButtons [i]);						
						amelia.anim.SetBool ("ThrowingSpear", false);
						break;
				}
			}
		}*/
	}

	void DisplayTouchList() {
		string r = "";
		foreach (string s in touchedButtons)
			r += " " + s;
		text1.text = r;
	}
}
