using UnityEngine;
using System.Collections;

public class KnightSpawner : MonoBehaviour {
	public float frequency, timeToStart;
	float timeAcc;
	public GameObject knight;
	// Use this for initialization
	void Start () {
		timeAcc = timeToStart;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeAcc) {
			timeAcc = Time.time + frequency;
			Instantiate (knight, transform.position, Quaternion.identity);
		}
	}
}
