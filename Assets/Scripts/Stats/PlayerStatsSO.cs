using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("生存")]
    public int maxHP = 100;
    public int currentHP;

    [Header("移动")]
    public float moveSpeed = 5f;

    [Header("防御")]
    public float armor = 0f;

    /* 重置到初始值 */
    public void ResetToDefault()
    {
        currentHP = maxHP;
    }

    /* 受击示例：伤害先扣护甲再扣血 */
    public void TakeDamage(float damage)
    {
        float actualDamage = Mathf.Max(damage - armor, 0);
        currentHP = (int)Mathf.Clamp(currentHP - actualDamage, 0, maxHP);  //将数值限制在[0, maxHP]内
    }
}
