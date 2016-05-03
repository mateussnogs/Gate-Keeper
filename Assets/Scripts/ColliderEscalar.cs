using UnityEngine;
using System.Collections;

public class ColliderEscalar : MonoBehaviour {
	public static int ameliaDetected = 0;
	public string altura;
	GameObject jumpButton, escadaButton;
	// Use this for initialization
	void Start () {
		jumpButton = GameObject.FindGameObjectWithTag ("JumpButton");
		escadaButton = GameObject.FindGameObjectWithTag ("EscadaButton");
		escadaButton.GetComponent<Button> ().SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (gameObject.tag == "ColliderEscadaParar") {
			if (other.gameObject.tag == "Amelia")
				other.GetComponent<Amelia> ().StopClimbing ();
		} else if (gameObject.tag == "ColliderEscalar") {
			if (other.gameObject.tag == "Amelia") {
				if (++ameliaDetected == 2) {
					jumpButton.GetComponent<Button> ().SetActive (false);
					escadaButton.GetComponent<Button> ().SetActive (true);
					if (altura == "cima")
						other.GetComponent<Amelia> ().canClimbDown = true;
					else if (altura == "baixo") {
						other.GetComponent<Amelia> ().canClimbUp = true;
					}
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
	}

	void OnTriggerExit2D(Collider2D other) {
		if (gameObject.tag == "ColliderEscalar") {
			if (other.gameObject.tag == "Amelia") {	
				jumpButton.GetComponent<Button> ().SetActive (true);
				escadaButton.GetComponent<Button> ().SetActive (false);
				ameliaDetected--;
				if (altura == "cima")
					other.GetComponent<Amelia> ().canClimbDown = false;
				else if (altura == "baixo")
					other.GetComponent<Amelia> ().canClimbUp = false;
			}
		}
	}
}
