using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Vector2 dir;
    private Rigidbody2D rb;
    public float speed;
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	
	}
    public void Move(Vector2 dir)
    {
        rb.velocity = dir.normalized * speed;
    }

}
