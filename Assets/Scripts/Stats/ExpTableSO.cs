using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player/ExpTable")]
public class ExpTableSO : ScriptableObject
{
    [Header("������ʽ")]
    public UpgradeType type = UpgradeType.Fixed;

    [Header("�̶���")]
    public int[] requiredExp;  //ÿ�����辭��

    [Header("��ʽ����")]
    public float baseExp = 100f;
    public float expMultiplier = 1.5f;  // = baseExp * (level^expMultiplier)

    public enum UpgradeType { Fixed, Formula }

    public int GetExpToNext(int currentLevel)
    {
        if (type == UpgradeType.Fixed)
            return requiredExp[Mathf.Clamp(currentLevel, 0, requiredExp.Length - 1)];

        // ��ʽ��baseExp * level^multiplier
        return Mathf.RoundToInt(baseExp * Mathf.Pow(currentLevel, expMultiplier));
    }
}
