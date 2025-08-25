using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBaseSO stats;
    [SerializeField] public Animator anim;  //�������
    private EnemyBaseSO runtimeStats;  //����ʱ���Ը���
    private Rigidbody2D rb;  //�������
    [SerializeField] private LayerMask playerLayer;
    private float currentHP;
    private bool isInvincible = true;  //�Ƿ��޵�
    private bool canAttack = true;  //�Ƿ���Թ���

    private Dictionary<EnemyStateType, IState> states = new Dictionary<EnemyStateType, IState>();
    private IState currentState;

    private Transform player;

    void OnEnable()
    {
        this.transform.localScale = Vector3.one;
        this.runtimeStats = Instantiate(this.stats);  //ʵ�������Ը���
        this.isInvincible = true;  //�����޵�״̬
        StartCoroutine(this.CancelInvincible());
    }

    void OnDisable()
    {
        this.runtimeStats = null;  //�������Ը���
    }

    void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        //ʵ��������״̬
        this.states.Add(EnemyStateType.Idle, new EnemyIdleState(this));
        this.states.Add(EnemyStateType.Chase, new EnemyChaseState(this));
        this.states.Add(EnemyStateType.Attack, new EnemyAttackState(this));
        this.states.Add(EnemyStateType.Hurt, new EnemyHurtState(this));
        this.states.Add(EnemyStateType.Death, new EnemyDeathState(this));
        this.TransitionState(EnemyStateType.Idle);  //��ʼ״̬Ϊ����
    }

    public void GetPlayerTransform()  //��ȡ���λ��
    {
        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(this.transform.position, this.runtimeStats.detectRadius, this.playerLayer);
        if (chaseColliders.Length > 0)
        {
            this.player = chaseColliders[0].transform;

            float distance = Vector3.Distance(this.transform.position, this.player.position);
            if (distance <= this.runtimeStats.attackRange)
            {
                this.TransitionState(EnemyStateType.Attack);  //�л�������״̬
            }
            else this.TransitionState(EnemyStateType.Chase);  //�л���׷��״̬
        }
        else
        {
            this.player = null;
            this.TransitionState(EnemyStateType.Idle);  //�л�������״̬
        }
    }

    public void ChasePlayer()  //׷�����
    {
        if (this.player != null)
        {
            Vector3 direction = (this.player.position - this.transform.position).normalized;
            this.transform.position += direction * this.runtimeStats.moveSpeed * Time.deltaTime;
        }
        else
        {
            this.TransitionState(EnemyStateType.Idle);  //�л�������״̬
        }
    }

    public void TransitionState(EnemyStateType type)
    {
        if (this.currentState != null)  //�����ǰ״̬��Ϊ�գ��˳���ǰ״̬
        {
            this.currentState.OnExit();
        }
        this.currentState = this.states[type];  //�л�״̬
        this.currentState.OnEnter();
    }

    public void TakeDamage(float damage, Vector2 hitDir)  //�ܵ��˺�
    {
        if (this.isInvincible) return;  //�޵�״̬������
        this.currentHP -= damage;
        if (this.currentHP <= 0)
        {
            this.currentHP = 0;
            this.isInvincible = true;  //�����޵�״̬����ֹ�ظ���������
            this.TransitionState(EnemyStateType.Death);  //�л�������״̬
        }
        else
        {
            this.TransitionState(EnemyStateType.Hurt);  //�л�������״̬
            this.Knockback(hitDir);  //����
        }
    }

    public void Knockback(Vector2 hitDir)  //����
    {
        rb.velocity = hitDir.normalized * this.runtimeStats.knockbackForce;
        StartCoroutine(StopKnockback());
    }

    private IEnumerator StopKnockback()  //ֹͣ����
    {
        yield return new WaitForSeconds(this.runtimeStats.knockbackTime);
        this.rb.velocity = Vector2.zero;
    }

    public void Die()
    {
        this.currentState = this.states[EnemyStateType.Idle];  //��ʼ״̬Ϊ����
        anim.Rebind();  //���ö�������Ȼ�ٴ����ɺ��޷�������Ⱦ
        ComboCounter.Instance.AddKill();  //֪ͨ����ϵͳ
        MultiObjectPool.Instance.Return(this.runtimeStats.enemyName, gameObject);  //���յ��˶���
        //WeaponUpgrade.Instance.AddKill();  //֪ͨ��������ϵͳ
    }

    private IEnumerator CancelInvincible()  //ȡ���޵�״̬
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

public enum EnemyStateType  //���ö�ٿ��԰���״̬��ʶ��ǰ״̬
{
    Idle,
    Chase,
    Attack,
    Hurt,
    Death
}