using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {
    private GameObject gate, player;
    private Transform target;
    private float gateDistance, playerDistance;
    private IEnemyStates states;

    public EnemyCollisionTreatment enemy;
    public Transform enemyTransform;
   
    void Start()
    {
        states = new IEnemyStates(enemy.speed);

        gate = GameObject.FindGameObjectWithTag("Gate");
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(CheckState());
      
}
    IEnumerator CheckState()
    {
        while (true)
        {
            if (enemy.isPursuing)
            {
                AttackToPursue();
            }
            else if (enemy.isAttacking)
            {
                PursueToAttack();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    void PursueToAttack()
    {
        states.Attack();
    }
    void AttackToPursue()
    {

        enemy.isAttacking = false;
        enemy.isPursuing = true;

        gateDistance = Mathf.Abs(gate.transform.position.x - enemyTransform.position.x);
        playerDistance = Mathf.Abs(player.transform.position.x - enemyTransform.position.x);

        if (gateDistance > playerDistance) states.Pursue(player.transform, enemyTransform.transform);
        else states.Pursue(gate.transform, enemyTransform.transform);
        
    }
  

}
