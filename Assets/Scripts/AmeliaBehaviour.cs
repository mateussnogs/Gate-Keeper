    using UnityEngine;
using System.Collections;

public class AmeliaBehaviour: MonoBehaviour {
    public float speed;

    private Vector2 dir;
    private Rigidbody2D rb;
    private Animator anim;
    private IEnumerator coroutineInstance;
    private Transform ameliaTransform;
    private float lookAt;



    public Battle weapon;
    
    void Start()
    {
        ameliaTransform = transform.parent.transform;
        coroutineInstance = null;
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }
    public void Move(float dirX)
    {
        dir = new Vector2(dirX, 0);
        rb.velocity = dir.normalized * speed;

        if (dirX != 0)
        {
            lookAt = Mathf.Rad2Deg * Mathf.Acos((-1) * Mathf.RoundToInt(dirX));
            anim.SetBool("Running", true);
        }
        else
            anim.SetBool("Running", false);
        ameliaTransform.eulerAngles = new Vector3(0, lookAt, 0);
    }
    public void AttackTrigger()
    {
        if (coroutineInstance == null)
        {
            coroutineInstance = weapon.Attack(anim);
            StartCoroutine(coroutineInstance);
            coroutineInstance = null;
        }
    }
    


}
