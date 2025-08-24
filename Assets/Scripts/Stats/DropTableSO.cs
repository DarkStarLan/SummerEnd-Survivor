using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Drop/Table")]
public class DropTableSO : ScriptableObject
{
    [Header("�������")]
    [Range(0, 1)] public float silverChance = 0.35f;
    [Range(0, 1)] public float goldChance = 0.15f;
    [Range(0, 1)] public float diamondChance = 0.05f;

    [Header("�����ֵ")]
    public int silverValue = 1;
    public int goldValue = 5;
    public int diamondValue = 20;
}

public enum CurrencyType
{
    Silver,
    Gold,
    Diamond
}