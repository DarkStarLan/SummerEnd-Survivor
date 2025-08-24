using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon/WeaponBase")]
public class WeaponBaseSO : ScriptableObject
{
    [Header("通用")]
    public string weaponName = "Weapon";
    public GameObject weaponPrefab;  //武器预制体

    [Header("发射")]
    public BulletStatsSO bulletStats;  //弹药数据
    public float fireRate = 1f;  //每秒发射次数

    [Header("数值")]
    public float damage = 10f;  //伤害
    public float cooldown = 1f;  //冷却（秒）

    [Header("阵型")]
    public WeaponShape shape = WeaponShape.Circle;
    public int projectileCount = 3;  //圆/线/锥用得到
    public float spacing = 30f;  //线或锥的角度间隔
    public float radius = 2f;  //半径（单位：米）
    public float rotationSpeed = 90f;  //转速:度/秒
}

public enum WeaponShape { None, Circle, Line, Cone }   // 可继续扩展