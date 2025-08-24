using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Level/LevelTable")]
public class WeaponLevelSO : ScriptableObject
{
    [Header("每级所需击杀数")]
    public int[] killThreshold = { 5, 15, 30, 50 };

    [Header("升级数值")]
    public int[] weaponCount = { 3, 4, 5, 6 };   //武器数量
    public float[] rotationSpeed = { 90, 120, 150, 180 };  //度/秒
    public float[] radius = { 2f, 2.5f, 3f, 3.5f };  //阵型半径
}
