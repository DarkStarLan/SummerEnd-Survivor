using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("�������")]
    private Transform tf;
    private Rigidbody2D rb;
    private PlayerControls controls;
    private Vector2 moveInput;

    [SerializeField] private PlayerStatsSO stats;

    [Header("�˶�Ч��")]

    [Header("�ƶ�")]
    public float maxSpeed = 6f;
    public float acceleration = 22f;  //���ٶ�
    [ReadOnly] public float speed;

    [Header("���")]
    public float dashSpeed = 24f;  //����ٶ�
    public float dashTime = 0.2f;  //��̳���ʱ��
    public float dashCooldown = 0.5f;  //��ȴ
    private bool isDashing = false;
    private bool canDash = true;

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Dash.performed += OnDashInput;  //��ť��������һ˲�䴥�����¼�
    }
    void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Dash.performed -= OnDashInput;  //��ť��������һ˲�䴥�����¼�
    }

    void Awake()
    {
        this.tf = this.transform;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.controls = new PlayerControls();
        stats.ResetToDefault();  //������Ѫ
    }

    void Update()
    {
        this.moveInput = this.controls.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        //ƽ�����ٶȣ��ӵ�ǰ�ٶȲ�ֵ��Ŀ���ٶ�
        Vector2 targetVel = moveInput * maxSpeed;
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, acceleration * Time.fixedDeltaTime);
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;  //��ֵƽ��

        this.speed = this.rb.velocity.magnitude;
    }

    public void ReceiveDamage(float dmg)
    {
        stats.TakeDamage(dmg);
        if (stats.currentHP <= 0) Debug.Log("Game Over");
    }

    void OnDashInput(InputAction.CallbackContext _)
    {
        if (this.canDash && this.moveInput != Vector2.zero)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = moveInput.normalized * dashSpeed;

        /* ������Բ��޵�֡����Ч����Ч */
        // Invincible = true;
        // StartDashEffect();

        yield return new WaitForSeconds(this.dashTime);

        /* ������̣����Ա������Ի����� */
        rb.velocity = Vector2.zero;   //�Ƴ�����

        // Invincible = false;
        // StopDashEffect();

        isDashing = false;

        /* ��ȴ */
        yield return new WaitForSeconds(this.dashCooldown - this.dashTime);
        canDash = true;
    }
}
