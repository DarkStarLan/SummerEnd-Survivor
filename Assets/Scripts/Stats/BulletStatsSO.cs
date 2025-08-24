using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Bullet/BulletStats")]
public class BulletStatsSO : ScriptableObject
{
    [Header("ͨ��")]
    public string bulletName = "Bullet";  //�ӵ�����
    public BulletType bulletType = BulletType.Null;  //�ӵ�����
    public int damage = 10;
    public float speed = 10f;
    public float lifeTime = 3f;
    public int maxPenetration = 0;  //0=����͸

    [Header("��Ч")]
    public GameObject hitEffect;  //��ѡ��������Ч
}

public enum BulletType
{
    Null,
    Arrow
}