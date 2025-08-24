using System.Collections.Generic;
using UnityEngine;

public class WeaponFormation : MonoBehaviour
{
    [ReadOnly] public WeaponBaseSO runtimeWeapon;  //武器数据
    [SerializeField] private Transform pivot;  //发射中心
    private readonly List<GameObject> points = new();
    private Transform tf;
    private float nextFireTime;  //下次发射时间

    void Start()
    {
        this.nextFireTime = Time.time + (1f / runtimeWeapon.fireRate);
        this.RefreshFormation();
        if (this.runtimeWeapon.bulletStats == null) this.nextFireTime = -1f;  //如果没有子弹，则不发射
    }

    void Awake()
    {
        this.tf = this.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) this.GetComponent<WeaponUpgrade>()?.AddKill();  //测试用，按R升级武器

        if (points.Count != this.runtimeWeapon.projectileCount) this.RefreshFormation();
        this.tf.Rotate(0, 0, this.runtimeWeapon.rotationSpeed * Time.deltaTime);

        //发射子弹
        if (this.nextFireTime > 0f && Time.time >= this.nextFireTime)
        {
            foreach (var p in this.points)
            {
                print(this.runtimeWeapon.bulletStats);
                Fire(p.transform.position);  //发射子弹
            }
            this.nextFireTime = Time.time + (1f / this.runtimeWeapon.fireRate);  //用fireRate计算
        }
    }

    public void RefreshFormation()  //刷新阵型
    {
        foreach (var p in points) Destroy(p);
        points.Clear();

        if (this.runtimeWeapon.shape == WeaponShape.Circle)  //圆形阵型
            ArrangeCircle();

        this.ShowTrail(true);  //显示拖尾
    }

    void Fire(Vector2 pos)
    {
        Vector2 dir = (pos - (Vector2)this.pivot.position).normalized;

        var bullet = MultiObjectPool.Instance.Get(this.runtimeWeapon.bulletStats.bulletType.ToString());
        bullet.transform.position = pos;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * this.runtimeWeapon.bulletStats.speed;
        bullet.GetComponent<Bullet>().stats = this.runtimeWeapon.bulletStats;  //设置子弹属性
    }

    public void ShowTrail(bool show)
    {
        foreach (GameObject p in this.points)
        {
            var trail = p.transform.Find("TrailPoint").GetComponent<TrailRenderer>();
            if (trail != null) trail.enabled = show;
        }
    }

    void ArrangeCircle()
    {
        float step = 360f / this.runtimeWeapon.projectileCount;
        for (int i = 0; i < this.runtimeWeapon.projectileCount; ++i)
        {
            float angle = i * step * Mathf.Deg2Rad;
            Vector3 pos = this.pivot.position +
                          new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * this.runtimeWeapon.radius;
            GameObject p = Instantiate(this.runtimeWeapon.weaponPrefab, pos, Quaternion.identity, this.pivot);
            Vector2 dir = (pos - this.pivot.position).normalized;
            p.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
            p.GetComponent<WeaponController>().weaponStats = this.runtimeWeapon;  //设置武器属性
            p.SetActive(true);
            points.Add(p);
        }
    }
}
