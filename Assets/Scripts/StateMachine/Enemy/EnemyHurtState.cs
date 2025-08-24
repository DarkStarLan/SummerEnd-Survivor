using UnityEngine;

public class EnemyHurtState : IState
{
    private Enemy enemy;

    public EnemyHurtState(Enemy enemy)
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
