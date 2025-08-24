using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Bullet/BulletStats")]
public class BulletStatsSO : ScriptableObject
{
    [Header("通用")]
    public string bulletName = "Bullet";  //子弹名称
    public BulletType bulletType = BulletType.Null;  //子弹类型
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 3f;
    public int maxPenetration = 0;  //0=不穿透

    [Header("特效")]
    public GameObject hitEffect;  //可选：击中特效
}

public enum BulletType
{
    Null,
    Arrow
}