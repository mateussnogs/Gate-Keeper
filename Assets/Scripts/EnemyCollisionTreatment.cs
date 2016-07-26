using UnityEngine;
using System.Collections;

public class EnemyCollisionTreatment : MonoBehaviour {

    [HideInInspector]
    public bool isAttacking, isPursuing;
    public float speed;


    void Start()
    {
        isAttacking = false;
        isPursuing = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!(col.gameObject.CompareTag("Enemy")))
        {
            isAttacking = true;
            isPursuing = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (!(col.gameObject.CompareTag("Enemy")))
        {
            isAttacking = true;
            isPursuing = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (!(col.gameObject.CompareTag("Enemy")))
        {
            isAttacking = false;
            isPursuing = true;
        }
    }
    


}
