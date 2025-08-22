using System.Collections.Generic;
using UnityEngine;

public class WeaponFormation : MonoBehaviour
{
    [SerializeField] private WeaponBaseSO weapon;  //武器数据
    [SerializeField] private Transform pivot;  //发射中心
    private readonly List<GameObject> points = new();
    private Transform tf;
    private float nextFireTime;  //下次发射时间

    void Start()
    {
        this.nextFireTime = Time.time + (1f / weapon.fireRate);
        RefreshFormation();
        if (this.weapon.bulletStats.bulletType == BulletType.Null) this.nextFireTime = -1f;  //如果没有子弹类型，则不发射
    }

    void Awake()
    {
        this.tf = this.transform;
    }

    void Update()
    {
        if (points.Count != this.weapon.projectileCount) RefreshFormation();
        this.tf.Rotate(0, 0, this.weapon.rotationSpeed * Time.deltaTime);

        //发射子弹
        if (this.nextFireTime > 0f && Time.time >= this.nextFireTime)
        {
            foreach (var p in this.points)
            {
                Fire(p.transform.position);  //发射子弹
            }
            this.nextFireTime = Time.time + (1f / this.weapon.fireRate);  //用fireRate计算
        }
    }

    void RefreshFormation()  //刷新阵型
    {
        foreach (var p in points) DestroyImmediate(p);
        points.Clear();

        if (this.weapon.shape == WeaponShape.Circle)  //圆形阵型
            ArrangeCircle();
    }

    void Fire(Vector2 pos)
    {
        Vector2 dir = (pos - (Vector2)this.pivot.position).normalized;

        var bullet = MultiObjectPool.Instance.Get(this.weapon.bulletStats.bulletType);
        bullet.transform.position = pos;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * this.weapon.bulletStats.speed;
        bullet.GetComponent<Bullet>().stats = this.weapon.bulletStats;  //设置子弹属性
    }


    void ArrangeCircle()
    {
        float step = 360f / this.weapon.projectileCount;
        for (int i = 0; i < this.weapon.projectileCount; ++i)
        {
            float angle = i * step * Mathf.Deg2Rad;
            Vector3 pos = this.pivot.position +
                          new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * this.weapon.radius;
            GameObject p = Instantiate(this.weapon.weaponPrefab, pos, Quaternion.identity, this.pivot);
            Vector2 dir = (pos - this.pivot.position).normalized;
            p.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
            p.SetActive(true);
            points.Add(p);
        }
    }
}
