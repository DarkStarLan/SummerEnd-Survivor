using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Enemy/EnemyBase")]
public class EnemyBaseSO : ScriptableObject
{
    [Header("通用")]
    public string enemyName = "Enemy";
    public GameObject prefab;  //敌人预制体

    [Header("生存")]
    public float maxHP = 30f;

    [Header("战斗")]
    public float damage = 10f;        //攻击/子弹伤害
    public float attackRange = 2f;    //触发攻击的距离
    public float attackInterval = 1f; //两次攻击间隔
    public float moveSpeed = 2f;      //追击/飞行速度
    public float detectRadius = 5f;   //发现玩家的距离

    [Header("击退")]
    public float knockbackForce = 8f;
    public float knockbackTime = 0.2f;
}