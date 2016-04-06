using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	Vector3 origin;
	Vector3 destiny;
	float startTime;
	float journeyLength;
	bool pressed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.RightArrow))
			pressed = false;
		if (Input.GetKeyDown (KeyCode.RightArrow) || pressed) {
			pressed = true;
			startTime = Time.time;
			origin = transform.position;
			destiny = origin + new Vector3 (0.2f, 0, 0);
			journeyLength = Vector3.Distance (origin, destiny);
		}
		transform.position = Vector3.Lerp (origin, destiny, journeyLength);	
	}
}
