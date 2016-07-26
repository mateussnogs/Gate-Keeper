    using UnityEngine;
using System.Collections;

public class AmeliaBehaviour: MonoBehaviour {
    public float speed;

    private Vector2 dir;
    private Rigidbody2D rb;
    private Animator anim;
    private IEnumerator coroutineInstance;

    [HideInInspector]
    public bool isAttacking;

    void Start()
    {
        coroutineInstance = null;
        rb = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
        isAttacking = false;
    }

    public void Move(float dirX)
    {
        dir = new Vector2(dirX, 0);
        rb.velocity = dir.normalized * speed;
    }
    public void AttackTrigger()
    {
        if (coroutineInstance == null)
        {
            coroutineInstance = Attack();
            StartCoroutine(coroutineInstance);
        }
    }
    public IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        isAttacking = true;
        //pega o tempo do ataque pra habilitar novamente
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        coroutineInstance = null;
        isAttacking = false;

    }
    


}
