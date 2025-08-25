using System.Collections;
using UnityEngine;

public class EnemyAttackState : IState
{
    private Enemy enemy;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        this.enemy.GetPlayerTransform();
        this.enemy.StartCoroutine(this.enemy.AttackCoroutine());
    }

    public void OnFixedUpdate()
    {
        
    }
    
    public void OnExit()
    {
        
    }
}
