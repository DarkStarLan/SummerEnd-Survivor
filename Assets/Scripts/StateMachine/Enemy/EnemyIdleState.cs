using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        this.enemy.GetPlayerTransform();
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
