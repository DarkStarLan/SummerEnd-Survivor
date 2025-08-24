using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Spawner/Wave")]
public class SpawnerWaveSO : ScriptableObject
{
    [Header("����ʱ�� (��)")]
    public float waveDuration = 30f;

    [Header("�ܶ����� (0-1)")]
    public AnimationCurve densityCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("����Ԥ����")]
    public EnemyBaseSO[] enemyTypes;
}
