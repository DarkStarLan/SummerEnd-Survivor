using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Player/ExpTable")]
public class ExpTableSO : ScriptableObject
{
    [Header("升级方式")]
    public UpgradeType type = UpgradeType.Fixed;

    [Header("固定表")]
    public int[] requiredExp;  //每级所需经验

    [Header("公式曲线")]
    public float baseExp = 100f;
    public float expMultiplier = 1.5f;  // = baseExp * (level^expMultiplier)

    public enum UpgradeType { Fixed, Formula }

    public int GetExpToNext(int currentLevel)
    {
        if (type == UpgradeType.Fixed)
            return requiredExp[Mathf.Clamp(currentLevel, 0, requiredExp.Length - 1)];

        // 公式：baseExp * level^multiplier
        return Mathf.RoundToInt(baseExp * Mathf.Pow(currentLevel, expMultiplier));
    }
}
