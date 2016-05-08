using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {
	public Amelia amelia;
	AtkButton axeButton, swordButton, spearButton;
	// Use this for initialization
	void Start () {
		axeButton = GameObject.FindGameObjectWithTag ("AxeButton").GetComponent<AtkButton> ();
		swordButton = GameObject.FindGameObjectWithTag ("SwordButton").GetComponent<AtkButton>();
		spearButton = GameObject.FindGameObjectWithTag ("SpearButton").GetComponent<AtkButton>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			amelia.SwitchWeapon (true);
		else if(Input.GetKeyDown(KeyCode.DownArrow))
			amelia.SwitchWeapon (false);

		if (Input.GetKeyDown (KeyCode.Space))
			amelia.Jump ();
		//else if (Input.GetKeyDown (KeyCode.Z))
		//	amelia.Attack ();
		else if (Input.GetKeyDown (KeyCode.Z))
			axeButton.OnTouch ();
		else if (Input.GetKeyDown (KeyCode.X))
			swordButton.OnTouch ();
		else if (Input.GetKeyDown (KeyCode.C))
			spearButton.OnTouch ();
		
		else if (Input.GetKeyDown (KeyCode.LeftArrow))
			amelia.movingLeft = true;
		else if (Input.GetKeyDown (KeyCode.RightArrow))
			amelia.movingRight = true;

		if (Input.GetKeyUp (KeyCode.LeftArrow))
			amelia.movingLeft = false;
		if (Input.GetKeyUp (KeyCode.RightArrow))
			amelia.movingRight = false;


		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (amelia.canClimbUp) {
				amelia.climbing = true;
				amelia.climbingUp = true;
			}
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (amelia.canClimbDown) {
				amelia.climbing = true;
				amelia.climbingDown = true;
			}
		}
			

	}
}
