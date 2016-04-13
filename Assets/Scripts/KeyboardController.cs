using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {
	public Amelia amelia;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			amelia.SwitchWeapon (true);
		else if(Input.GetKeyDown(KeyCode.DownArrow))
			amelia.SwitchWeapon (false);

		if (Input.GetKeyDown (KeyCode.Space))
			amelia.Jump ();
		else if (Input.GetKeyDown (KeyCode.Z))
			amelia.Attack ();
		
		else if (Input.GetKeyDown (KeyCode.LeftArrow))
			amelia.movingLeft = true;
		else if (Input.GetKeyDown (KeyCode.RightArrow))
			amelia.movingRight = true;

		if (Input.GetKeyUp (KeyCode.LeftArrow))
			amelia.movingLeft = false;
		if (Input.GetKeyUp (KeyCode.RightArrow))
			amelia.movingRight = false;
	}
}
