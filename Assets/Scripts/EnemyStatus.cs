using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

    [HideInInspector]
    public bool isAttacking, isPursuing;
    public float speed;
   
    public GameObject targetObject;


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
       //     targetObject = col.gameObject;
        }
        else if(col.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            isPursuing = false;
         //   targetObject = col.gameObject;
            
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Gate"))
        {
            isAttacking = true;
            isPursuing = false;

//            targetObject = col.gameObject;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            isPursuing = false;

           // targetObject = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (!(col.gameObject.CompareTag("Enemy")))
        {
            isAttacking = false;
            isPursuing = true;

           // targetObject = null;
        }
    }
    


}
