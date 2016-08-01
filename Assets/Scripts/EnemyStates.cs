using UnityEngine;
using System.Collections;

public class EnemyStates : MonoBehaviour {
    private Vector2 newPos;
    //diferente da velocidade do player, quero saber o tamanho do passo
    public float speed;
    public float offset = 1.0f;
    public Rigidbody2D rb;

    private Animator anim;
    private Vector2 dir;
    private GameObject enemy;
    private float lookAt;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        enemy = transform.parent.gameObject;
        dir = new Vector2(Mathf.Cos(enemy.transform.rotation.x), 0);

    }

    void Update()
    {
       
        //coloquei *(-1) pq o sprite esta virado para o lado contrario
        lookAt = Mathf.Rad2Deg * Mathf.Acos((-1) * Mathf.RoundToInt(dir.x));
        enemy.transform.eulerAngles = new Vector3(0, lookAt, 0);
    }
	public void Pursue(Transform target)
    {
        dir = new Vector2(target.position.x - enemy.transform.position.x, 0);
        dir.Normalize();
        
        rb.velocity = dir * speed;
        anim.SetBool("Walking", true);
        print("direção: " + dir);
    }

    public IEnumerator Attack()
    {
        anim.SetBool("Walking", false);
        while (true)
        {
            //substituir por tempo de ataque
            yield return new WaitForSeconds(1.0f);
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Attack");
        }
       
    }
    public void GetHit()
    {
        anim.SetTrigger("Damaged");
    }

}
