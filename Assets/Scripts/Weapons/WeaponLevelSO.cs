using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Level/LevelTable")]
public class WeaponLevelSO : ScriptableObject
{
    [Header("ÿ�������ɱ��")]
    public int[] killThreshold = { 5, 15, 30, 50 };

    [Header("������ֵ")]
    public int[] weaponCount = { 3, 4, 5, 6 };   //��������
    public float[] rotationSpeed = { 90, 120, 150, 180 };  //��/��
    public float[] radius = { 2f, 2.5f, 3f, 3.5f };  //���Ͱ뾶
}
