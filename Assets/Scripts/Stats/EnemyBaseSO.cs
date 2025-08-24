using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Enemy/EnemyBase")]
public class EnemyBaseSO : ScriptableObject
{
    [Header("ͨ��")]
    public string enemyName = "Enemy";
    public GameObject prefab;  //����Ԥ����

    [Header("����")]
    public float maxHP = 30f;

    [Header("ս��")]
    public float damage = 10f;        //����/�ӵ��˺�
    public float attackRange = 2f;    //���������ľ���
    public float attackInterval = 1f; //���ι������
    public float moveSpeed = 2f;      //׷��/�����ٶ�
    public float detectRadius = 5f;   //������ҵľ���

    [Header("����")]
    public float knockbackForce = 8f;
    public float knockbackTime = 0.2f;
}