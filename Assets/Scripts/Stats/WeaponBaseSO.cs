using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Weapon/WeaponBase")]
public class WeaponBaseSO : ScriptableObject
{
    [Header("ͨ��")]
    public string weaponName = "Weapon";
    public GameObject weaponPrefab;  //����Ԥ����

    [Header("����")]
    public BulletStatsSO bulletStats;  //��ҩ����
    public float fireRate = 1f;  //ÿ�뷢�����

    [Header("��ֵ")]
    public float damage = 10f;  //�˺�
    public float cooldown = 1f;  //��ȴ���룩

    [Header("����")]
    public WeaponShape shape = WeaponShape.Circle;
    public int projectileCount = 3;  //Բ/��/׶�õõ�
    public float spacing = 30f;  //�߻�׶�ĽǶȼ��
    public float radius = 2f;  //�뾶����λ���ף�
    public float rotationSpeed = 90f;  //ת��:��/��
}

public enum WeaponShape { None, Circle, Line, Cone }   // �ɼ�����չ