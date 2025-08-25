using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [ReadOnly] public WeaponBaseSO weaponStats;  // Weapon stats ScriptableObject
    [SerializeField] private HitEffect hitEffect;  // Hit effect prefab
    [SerializeField] private AudioClip audioHit;  // Hit sound effect

    private int enemyCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(this.weaponStats.damage, collision.transform.position - this.transform.position);
            AudioController.Instance.PlaySound(this.audioHit);
            ++this.enemyCount;
            if (this.enemyCount == 1)
            {
                this.hitEffect.PlayHit(); // 第一个敌人进入时执行
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            --this.enemyCount;
            if (this.enemyCount == 0)
            {
                this.hitEffect.StopHit(); // 最后一个敌人离开时执行
            }
        }
    }
}
