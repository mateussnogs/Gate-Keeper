using UnityEngine;
using System.Collections;

public class IEnemyStates : MonoBehaviour {
    private Vector2 newPos;
    //diferente da velocidade do player, quero saber o tamanho do passo
    public float speed;
    
    public IEnemyStates(float speed)
    {
        this.speed = speed;
    }

	public void Pursue(Transform target, Transform enemyPos)
    {
        newPos = new Vector2(target.position.x, enemyPos.position.y);
        enemyPos.position = Vector2.Lerp(enemyPos.position, newPos, speed);
    }
    public void Attack()
    {
        print("Ataque");
    }

}
