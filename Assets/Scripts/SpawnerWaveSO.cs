using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Spawner/Wave")]
public class SpawnerWaveSO : ScriptableObject
{
    [Header("波次时长 (秒)")]
    public float waveDuration = 30f;

    [Header("密度曲线 (0-1)")]
    public AnimationCurve densityCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("敌人预制体")]
    public EnemyBaseSO[] enemyTypes;
}
