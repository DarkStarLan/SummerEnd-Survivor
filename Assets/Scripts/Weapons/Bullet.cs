using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public BulletStatsSO stats;
    private float timer;  //计时器

    void OnEnable()  //因为要使用对象池，所以需要重置计时器
    {
        this.timer = 0f;
    }

    void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= this.stats.lifeTime)
            MultiObjectPool.Instance.Return(this.stats.bulletType, gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO：扣血逻辑
        MultiObjectPool.Instance.Return(this.stats.bulletType, gameObject);
    }
}
