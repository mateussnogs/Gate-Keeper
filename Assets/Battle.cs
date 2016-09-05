using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {
    private bool onSight;
    private GameObject target;
      
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            onSight = true;
            target = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            onSight = false;
            target = null;
        }
    }

    public IEnumerator Attack(Animator anim)
    {
        anim.SetTrigger("Attack");
        print("ataquei");
        if (target != null)
        {
            //Destroy(target.transform.parent.gameObject);
            target.GetComponent<EnemyStates>().GetHit();
            //um problema aqui. Ele não ataca se eu mantiver esse target null parado
            //Acontece por conta do inimigo não morrer
            //target = null;
        }
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);


    }
}
