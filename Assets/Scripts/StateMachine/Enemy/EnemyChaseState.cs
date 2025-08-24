using UnityEngine;

public class EnemyChaseState : IState
{
    private Enemy enemy;

    public EnemyChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        this.enemy.GetPlayerTransform();
        this.enemy.ChasePlayer();
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
