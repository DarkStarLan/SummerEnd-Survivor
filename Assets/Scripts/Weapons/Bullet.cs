using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public BulletStatsSO stats;
    private float timer;  //��ʱ��

    void OnEnable()  //��ΪҪʹ�ö���أ�������Ҫ���ü�ʱ��
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
        // TODO����Ѫ�߼�
        MultiObjectPool.Instance.Return(this.stats.bulletType, gameObject);
    }
}
