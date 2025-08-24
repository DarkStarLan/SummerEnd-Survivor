using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("玩家属性")]
    private Transform tf;
    private Rigidbody2D rb;
    private PlayerControls controls;
    private Vector2 moveInput;

    [SerializeField] private PlayerStatsSO stats;

    [Header("运动效果")]

    [Header("移动")]
    public float maxSpeed = 6f;
    public float acceleration = 22f;  //加速度
    [ReadOnly] public float speed;

    [Header("冲刺")]
    public float dashSpeed = 24f;  //冲刺速度
    public float dashTime = 0.2f;  //冲刺持续时间
    public float dashCooldown = 0.5f;  //冷却
    private bool isDashing = false;
    private bool canDash = true;

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Dash.performed += OnDashInput;  //按钮被按下这一瞬间触发的事件
    }
    void OnDisable()
    {
        controls.Player.Disable();
        controls.Player.Dash.performed -= OnDashInput;  //按钮被按下这一瞬间触发的事件
    }

    void Awake()
    {
        this.tf = this.transform;
        this.rb = this.GetComponent<Rigidbody2D>();
        this.controls = new PlayerControls();
        stats.ResetToDefault();  //开局满血
    }

    void Update()
    {
        this.moveInput = this.controls.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        //平滑加速度：从当前速度插值到目标速度
        Vector2 targetVel = moveInput * maxSpeed;
        rb.velocity = Vector2.MoveTowards(rb.velocity, targetVel, acceleration * Time.fixedDeltaTime);
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;  //插值平滑

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

        /* 这里可以插无敌帧、特效、音效 */
        // Invincible = true;
        // StartDashEffect();

        yield return new WaitForSeconds(this.dashTime);

        /* 结束冲刺：可以保留惯性或清零 */
        rb.velocity = Vector2.zero;   //移除惯性

        // Invincible = false;
        // StopDashEffect();

        isDashing = false;

        /* 冷却 */
        yield return new WaitForSeconds(this.dashCooldown - this.dashTime);
        canDash = true;
    }
}
