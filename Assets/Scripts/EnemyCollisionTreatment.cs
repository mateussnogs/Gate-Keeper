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
        if(col.gameObject.CompareTag("Gate"))
        {
            isAttacking = true;
            isPursuing = false;
        }
        else if(col.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            isPursuing = false;
            if (col.GetComponentInChildren<AmeliaBehaviour>().isAttacking)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Gate"))
        {
            isAttacking = true;
            isPursuing = false;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            isPursuing = false;
            if (col.GetComponentInChildren<AmeliaBehaviour>().isAttacking)
            {
                print("Destrui");
                Destroy(transform.parent.gameObject);
            }
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
