using UnityEngine;

public class EnemyDeathState : IState
{
    private Enemy enemy;

    public EnemyDeathState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        this.enemy.GetComponent<EnemyDrop>().OnDeath();  //µÙ¬‰ŒÔ∆∑
        this.enemy.anim.Play("Die");
        //this.enemy.Die();
    }

    public void OnUpdate()
    {
        
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
