using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {
    private GameObject gate, player;
    private Transform target;
    private float gateDistance, playerDistance;
    public EnemyStates states;

    public EnemyCollisionTreatment enemyStatus;
    private GameObject enemy;

    //variaveis de controle da corotina de ataque
    private IEnumerator coroutineInstance;
   
    void Start()
    {
        enemy = transform.parent.gameObject;

        gate = GameObject.FindGameObjectWithTag("Gate");
        player = GameObject.FindGameObjectWithTag("Player");

        coroutineInstance = null;
        StartCoroutine(CheckState());

    }
    IEnumerator CheckState()
    {
        //delay inicial para que o Find termine de popular os objetos
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (enemyStatus.isPursuing)
            {
                AttackToPursue();
            }
            else if (enemyStatus.isAttacking)
            {
                PursueToAttack();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    void PursueToAttack()
    {
        if (coroutineInstance == null)
        {
            coroutineInstance = states.Attack();
            StartCoroutine(coroutineInstance);
        }
        enemyStatus.isAttacking = true;
        enemyStatus.isPursuing = false;
    }

    void AttackToPursue()
    {
        gateDistance = Mathf.Abs(gate.transform.position.x - enemy.transform.position.x);
        playerDistance = Mathf.Abs(player.transform.position.x - enemy.transform.position.x);

        if (coroutineInstance != null)
        {
            StopCoroutine(coroutineInstance);
            coroutineInstance = null;
        }

        if (gateDistance > playerDistance) states.Pursue(player.transform);
        else states.Pursue(gate.transform);  
    }
  

}
