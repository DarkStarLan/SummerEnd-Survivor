using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBaseSO stats;
    [SerializeField] public Animator anim;  //动画组件
    private EnemyBaseSO runtimeStats;  //运行时属性副本
    private Rigidbody2D rb;  //刚体组件
    [SerializeField] private LayerMask playerLayer;
    private float currentHP;
    private bool isInvincible = true;  //是否无敌
    private bool canAttack = true;  //是否可以攻击

    private Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();
    private IState currentState;

    private Transform player;

    void OnEnable()
    {
        this.transform.localScale = Vector3.one;
        this.runtimeStats = Instantiate(this.stats);  //实例化属性副本
        this.isInvincible = true;  //重置无敌状态
        StartCoroutine(this.CancelInvincible());
    }

    void OnDisable()
    {
        this.runtimeStats = null;  //销毁属性副本
    }

    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        //实例化敌人状态
        this.states.Add(EnemyStateType.Idle, new EnemyIdleState(this));
        this.states.Add(EnemyStateType.Chase, new EnemyChaseState(this));
        this.states.Add(EnemyStateType.Attack, new EnemyAttackState(this));
        this.states.Add(EnemyStateType.Hurt, new EnemyHurtState(this));
        this.states.Add(EnemyStateType.Death, new EnemyDeathState(this));
        this.TransitionState(EnemyStateType.Idle);  //初始状态为空闲
    }

    public void GetPlayerTransform()  //获取玩家位置
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(this.transform.position, this.runtimeStats.detectRadius, this.playerLayer);
        if (chaseColliders.Length > 0)
        {
            this.player = chaseColliders[0].transform;

            float distance = Vector3.Distance(this.transform.position, this.player.position);
            if (distance <= this.runtimeStats.attackRange)
            {
                this.TransitionState(EnemyStateType.Attack);  //切换到攻击状态
            }
            else this.TransitionState(EnemyStateType.Chase);  //切换到追逐状态
        }
        else
        {
            this.player = null;
            this.TransitionState(EnemyStateType.Idle);  //切换到空闲状态
        }
    }

    public void ChasePlayer()  //追逐玩家
    {
        if (this.player != null)
        {
            Vector3 direction = (this.player.position - this.transform.position).normalized;
            this.transform.position += direction * this.runtimeStats.moveSpeed * Time.deltaTime;
        }
        else
        {
            this.TransitionState(EnemyStateType.Idle);  //切换到空闲状态
        }
    }

    public void TransitionState(EnemyStateType type)
    {
        if (this.currentState != null)  //如果当前状态不为空，退出当前状态
        {
            this.currentState.OnExit();
        }
        this.currentState = this.states[type];  //切换状态
        this.currentState.OnEnter();
    }

    public void TakeDamage(float damage, Vector2 hitDir)  //受到伤害
    {
        if (this.isInvincible) return;  //无敌状态不受伤
        this.currentHP -= damage;
        if (this.currentHP <= 0)
        {
            this.currentHP = 0;
            this.isInvincible = true;  //进入无敌状态，防止重复触发死亡
            this.TransitionState(EnemyStateType.Death);  //切换到死亡状态
        }
        else
        {
            this.TransitionState(EnemyStateType.Hurt);  //切换到受伤状态
            this.Knockback(hitDir);  //击退
        }
    }

    public void Knockback(Vector2 hitDir)  //击退
    {
        rb.velocity = hitDir.normalized * this.runtimeStats.knockbackForce;
        StartCoroutine(StopKnockback());
    }

    private IEnumerator StopKnockback()  //停止击退
    {
        yield return new WaitForSeconds(this.runtimeStats.knockbackTime);
        this.rb.velocity = Vector2.zero;
    }

    public void Die()
    {
        this.currentState = this.states[EnemyStateType.Idle];  //初始状态为空闲
        anim.Rebind();  //重置动画，不然再次生成后无法正常渲染
        ComboCounter.Instance.AddKill();  //通知连击系统
        MultiObjectPool.Instance.Return(this.runtimeStats.enemyName, gameObject);  //回收敌人对象
        //WeaponUpgrade.Instance.AddKill();  //通知武器升级系统
    }

    private IEnumerator CancelInvincible()  //取消无敌状态
    {
        yield return new WaitForSeconds(0.5f);
        this.isInvincible = false;
    }

    public void Attack()
    {
        if (this.player != null)
        {
            PlayerStats.Instance.TakeDamage(this.runtimeStats.damage);
        }
    }

    public IEnumerator AttackCoroutine()
    {
        if (this.currentState == this.states[EnemyStateType.Attack])
        {
            if (this.canAttack)
            {
                this.Attack();
                this.canAttack = false;
            }
        }
        yield return new WaitForSeconds(this.runtimeStats.attackInterval);
        this.canAttack = true;
    }

    void Start() => this.currentHP = runtimeStats.maxHP;

    void Update() => this.currentState.OnUpdate();
    void FixedUpdate() => this.currentState.OnFixedUpdate();
}

public enum EnemyStateType  //这个枚举可以帮助状态机识别当前状态
{
    Idle,
    Chase,
    Attack,
    Hurt,
    Death
}