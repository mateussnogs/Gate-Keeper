using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	public Amelia amelia;
	public float speed = 10;
	public GameObject target;
	Vector3 origin;
	Vector3 destiny;
	public Vector3 targetDir;
	// Use this for initialization
	void Start () {
		origin = transform.position;
		destiny = (target.transform.position - origin).normalized;
	}
	
	// Update is called once per frame//
	void Update () {
		//transform.position += targetDir * speed * Time.deltaTime;
		Vector3 moveDir = (GetComponent<Renderer>().bounds.center - transform.position);
		//ACHAR CENTRO DO TARGET
		//Vector3 moveDir = (target.transform.position - transform.position);
		transform.position = Vector3.MoveTowards (transform.position, moveDir, Time.deltaTime * speed);
	}
}
