    using UnityEngine;
using System.Collections;

public class AmeliaBehaviour: MonoBehaviour {

    private Vector2 dir;
    public Rigidbody2D rb;
    public float speed;

    public void Move(float dirX)
    {
        dir = new Vector2(dirX, 0);
        rb.velocity = dir.normalized * speed;
    }

    public void Attack()
    {
        print("Ataquei");
    }



}
