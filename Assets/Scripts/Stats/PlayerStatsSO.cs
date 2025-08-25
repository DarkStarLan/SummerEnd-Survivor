using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("����")]
    public int maxHP = 100;
    public int currentHP;

    [Header("�ƶ�")]
    public float moveSpeed = 5f;

    [Header("����")]
    public float armor = 0f;

    /* ���õ���ʼֵ */
    public void ResetToDefault()
    {
        currentHP = maxHP;
    }

    /* �ܻ�ʾ�����˺��ȿۻ����ٿ�Ѫ */
    public void TakeDamage(float damage)
    {
        float actualDamage = Mathf.Max(damage - armor, 0);
        currentHP = (int)Mathf.Clamp(currentHP - actualDamage, 0, maxHP);  //����ֵ������[0, maxHP]��
    }
}
